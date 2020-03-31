using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services;
using DataLayer;
using DataLayer.Tables;
using MatchmakerTest.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatchmakerTest
{
    [TestClass]
    public class MatchDbWriteTests
    {
        /// <summary>
        /// Создаю объекты аккаунта и матча.
        /// Записываю в БД.
        /// Проверяю что, данные полностью записались.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Test1()
        {
            //Arrange
            var dbContext = new InMemoryDatabaseFactory(nameof(MatchDbWriteTests)).Create();

            //Act
            Account account = AccountFactory.CreateSimpleAccount();
            await dbContext.Accounts.AddAsync(account);
            await dbContext.SaveChangesAsync();
            List<MatchResultForPlayer> playersResult = MatchResultFactory.Create(account);
            Match match = new Match
            {
                StartTime = DateTime.UtcNow,
                GameServerIp = "someString",
                GameServerUdpPort = 999,
                MatchResultForPlayers = playersResult
            };

            await dbContext.Matches.AddAsync(match);
            await dbContext.SaveChangesAsync();
                        
            //Assert
            var matchDb = await dbContext.Matches
                .Where(match1 => match1.Id == match.Id)
                .Include(match1 => match1.MatchResultForPlayers)
                .SingleAsync();

            var resultForPlayersDb = matchDb.MatchResultForPlayers;
            var resultForPlayerDb = resultForPlayersDb.Single();
            Assert.AreEqual(account.Id, resultForPlayerDb.AccountId);
        }
    }
}