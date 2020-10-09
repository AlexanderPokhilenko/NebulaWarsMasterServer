using AmoebaGameMatcherServer.Services.MatchFinishing;
using AmoebaGameMatcherServer.Services.Queues;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace MatchmakerTest.MatchmakerTests
{
    [TestClass]
    public class BattleRoyaleMatchFinisherServiceTests
    {
        private ApplicationDbContext dbContext;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Test")
                .Options;

            dbContext = new ApplicationDbContext(options);
        }

        [TestCleanup]
        public void Cleanup()
        {
            dbContext.Rollback();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        [DataRow(-1, 1, 1)]
        [DataRow(int.MaxValue, 1, 1)]
        public async Task UpdatePlayerMatchResultInDbAsync_InvalidAccountId_Exception(int accountId, int placeInBattle, int matchId)
        {
            //Arrange
            dbContext.Accounts.Add(new Account { Id = 1 });
            var service = new BattleRoyaleMatchFinisherService(dbContext,
                new Mock<IBattleRoyaleUnfinishedMatchesSingletonService>().Object,
                new Mock<IBattleRoyaleMatchRewardCalculatorService>().Object,
                new Mock<IWarshipRatingReaderService>().Object);

            //Act
            await service.UpdatePlayerMatchResultInDbAsync(accountId, placeInBattle, matchId);

            //Assert
            Assert.Fail("Exception was not thrown!");
        }
    }
}
