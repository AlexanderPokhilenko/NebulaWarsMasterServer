using System;
using System.Collections.Generic;
using System.Linq;
using AmoebaGameMatcherServer.Services.MatchFinishing;
using AmoebaGameMatcherServer.Services.Queues;
using DataLayer;
using DataLayer.Tables;
using Libraries.NetworkLibrary.Experimental;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NetworkLibrary.NetworkLibrary.Http;

namespace MatchmakerIntegrationTests
{
    [TestClass]
    public class BattleRoyaleMatchFinisherTests
    {
        private static readonly ApplicationDbContext DbContext = ApplicationDbContextProvider.GetContext();

        private static BattleRoyaleMatchFinisherService GetSetUpService(int lootboxReward = 0, int softCurrencyReward = 0, int ratingReward = 0)
        {
            var fakeMatchesSingleton = new Mock<IBattleRoyaleUnfinishedMatchesSingletonService>();
            fakeMatchesSingleton.Setup(s => s.IsPlayerInMatch(It.IsAny<string>(), It.IsAny<int>())).Returns(true);
            fakeMatchesSingleton.Setup(s => s.TryRemovePlayerFromMatch(It.IsAny<string>())).Returns(true);
            fakeMatchesSingleton.Setup(s => s.TryRemoveMatch(It.IsAny<int>())).Returns(true);

            var returnedReward = new MatchReward
            {
                LootboxPoints = lootboxReward,
                SoftCurrency = softCurrencyReward,
                WarshipRatingDelta = ratingReward
            };
            var fakeRewardCalculator = new Mock<IBattleRoyaleMatchRewardCalculatorService>();
            fakeRewardCalculator.Setup(s => s.Calculate(It.IsAny<int>(), It.IsAny<int>())).Returns(returnedReward);

            var fakeRatingReader = new Mock<IWarshipRatingReaderService>();
            fakeRatingReader.Setup(s => s.ReadWarshipRatingAsync(It.IsAny<int>())).ReturnsAsync(0);

            return new BattleRoyaleMatchFinisherService(DbContext,
                fakeMatchesSingleton.Object,
                fakeRewardCalculator.Object,
                fakeRatingReader.Object);
        }

        public BattleRoyaleMatchFinisherTests()
        {
            if (DbContext.Warships.Any() || DbContext.Accounts.Any() || DbContext.Transactions.Any() ||
                DbContext.MatchResults.Any() || DbContext.Matches.Any())
            {
                Cleanup();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            DbContext.Rollback();
            DbContext.Warships.RemoveRange(DbContext.Warships);
            DbContext.Accounts.RemoveRange(DbContext.Accounts);
            DbContext.Transactions.RemoveRange(DbContext.Transactions);
            DbContext.MatchResults.RemoveRange(DbContext.MatchResults);
            DbContext.Matches.RemoveRange(DbContext.Matches);
            DbContext.SaveChanges();
        }

        [TestMethod]
        [DataRow(2, 1, 2)]
        [DataRow(1, 5, 2)]
        [DataRow(2, 8, 1)]
        [DataRow(25, 3, 5)]
        public void UpdatePlayerMatchResult_ValidData_NormalResponse(int accountId, int place, int matchId)
        {
            //Arrange
            var warship = new Warship { Id = 1, AccountId = accountId, CurrentSkinTypeId = SkinTypeEnum.Hare, WarshipTypeId = WarshipTypeEnum.Hare };
            var account = new Account { Id = accountId, ServiceId = "", Username = "", Warships = new List<Warship> {warship}};
            var matchResult = new MatchResult { Id = 1, MatchId = matchId, Warship = warship };
            var match = new DataLayer.Tables.Match {Id = matchId, GameServerIp = "", MatchResults = new List<MatchResult> {matchResult}};
            DbContext.Accounts.Add(account);
            DbContext.Matches.Add(match);
            DbContext.SaveChanges();

            var service = GetSetUpService();

            //Act
            var result = service.UpdatePlayerMatchResultInDbAsync(accountId, place, matchId).Result;

            //Assert
            Assert.IsTrue(result, "Method returned false.");
            Assert.IsTrue(matchResult.IsFinished, "Match result was not marked as finished.");
            Assert.AreEqual(place, matchResult.PlaceInMatch, "Incorrect place in match.");
            Assert.AreEqual(accountId, matchResult.Transaction.AccountId, "Incorrect account id.");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        [DataRow(2, 1, 1, DisplayName = "Wrong account id")]
        [DataRow(1, 3, 2, DisplayName = "Wrong match id")]
        public void UpdatePlayerMatchResult_WrongData_Exception(int accountId, int place, int matchId)
        {
            //Arrange
            var warship = new Warship { Id = 1, AccountId = 1, CurrentSkinTypeId = SkinTypeEnum.Hare, WarshipTypeId = WarshipTypeEnum.Hare };
            var account = new Account { Id = 1, ServiceId = "", Username = "", Warships = new List<Warship> { warship } };
            var matchResult = new MatchResult { Id = 1, MatchId = 1, Warship = warship };
            var match = new DataLayer.Tables.Match { Id = 1, GameServerIp = "", MatchResults = new List<MatchResult> { matchResult } };
            DbContext.Accounts.Add(account);
            DbContext.Matches.Add(match);
            DbContext.SaveChanges();

            var service = GetSetUpService();

            //Act
            service.UpdatePlayerMatchResultInDbAsync(accountId, place, matchId).Wait();

            //Assert
            Assert.Fail("Exception was not raised.");
        }

        [TestMethod]
        [DataRow(10, 10, 10, DisplayName = "All positive rewards")]
        [DataRow(0, 5, 5, DisplayName = "Zero lootbox reward")]
        [DataRow(15, 0, 10, DisplayName = "Zero soft currency reward")]
        [DataRow(25, 25, 0, DisplayName = "Zero rating reward")]
        [DataRow(5, 5, -10, DisplayName = "Negative rating reward")]
        public void UpdatePlayerMatchResult_RewardCalculations_NormalResponse(int lootboxReward, int softCurrencyReward, int ratingReward)
        {
            //Arrange
            var warship = new Warship { Id = 1, AccountId = 1, CurrentSkinTypeId = SkinTypeEnum.Hare, WarshipTypeId = WarshipTypeEnum.Hare };
            var account = new Account { Id = 1, ServiceId = "", Username = "", Warships = new List<Warship> { warship } };
            var matchResult = new MatchResult { Id = 1, MatchId = 1, Warship = warship };
            var match = new DataLayer.Tables.Match { Id = 1, GameServerIp = "", MatchResults = new List<MatchResult> { matchResult } };
            DbContext.Accounts.Add(account);
            DbContext.Matches.Add(match);
            DbContext.SaveChanges();

            var service = GetSetUpService(lootboxReward, softCurrencyReward, ratingReward);

            //Act
            var result = service.UpdatePlayerMatchResultInDbAsync(1, 1, 1).Result;

            //Assert
            var transaction = matchResult.Transaction;
            Assert.IsTrue(result, "Method returned false.");
            Assert.IsFalse(transaction.WasShown, "Transaction was shown.");

            var increments = transaction.Increments;
            var decrements = transaction.Decrements;

            Assert.IsTrue(increments.All(i => i.MatchRewardTypeId == MatchRewardTypeEnum.RankingReward), "Incorrect reward type.");

            if (lootboxReward > 0)
            {
                var increment = increments.Single(i => i.IncrementTypeId == IncrementTypeEnum.LootboxPoints);
                Assert.AreEqual(lootboxReward, increment.Amount, "Incorrect lootbox increment.");
            }

            if (softCurrencyReward > 0)
            {
                var increment = increments.Single(i => i.IncrementTypeId == IncrementTypeEnum.SoftCurrency);
                Assert.AreEqual(softCurrencyReward, increment.Amount, "Incorrect soft currency increment.");
            }

            if (ratingReward > 0)
            {
                var increment = increments.Single(i => i.IncrementTypeId == IncrementTypeEnum.WarshipRating);
                Assert.AreEqual(ratingReward, increment.Amount, "Incorrect rating increment.");
            }
            else if(ratingReward < 0)
            {
                var decrement = decrements.Single(d => d.DecrementTypeId == DecrementTypeEnum.WarshipRating);
                Assert.AreEqual(-ratingReward, decrement.Amount, "Incorrect rating decrement.");
            }
        }

        [TestMethod]
        public void FinishMatchAndWriteToDb_ValidData_NormalResponse()
        {
            //Arrange
            var match = new DataLayer.Tables.Match { Id = 1, GameServerIp = "", MatchResults = new List<MatchResult>(10) };
            for (var i = 1; i <= 10; i++)
            {
                var account = new Account { Id = i, ServiceId = i.ToString(), Username = i.ToString() };
                var warship = new Warship { Id = i, Account = account, CurrentSkinTypeId = SkinTypeEnum.Hare, WarshipTypeId = WarshipTypeEnum.Hare };
                match.MatchResults.Add(new MatchResult { Id = i, Match = match, Warship = warship });
            }
            DbContext.Matches.Add(match);
            DbContext.SaveChanges();

            var service = GetSetUpService();

            //Act
            service.FinishMatchAndWriteToDbAsync(match.Id).Wait();

            //Assert
            Assert.IsTrue(match.MatchResults.All(r => r.IsFinished), "Match was not finished.");
        }
    }
}
