using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services;
using DataLayer;
using DataLayer.Tables;
using MatchmakerTest.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatchmakerTest
{
    [TestClass]
    public class MatchDataDbWriterServiceTests
    {
        /// <summary>
        /// При нормальных данных сервис запишет данные о бое в БД.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Test1()
        {
            //Arrange
            IDbContextFactory dbContextFactory = new InMemoryDatabaseFactory(nameof(MatchDataDbWriterServiceTests));
            var dbContext = dbContextFactory.Create();
            MatchDbWriterService matchDbWriterService = new MatchDbWriterService(dbContextFactory);
            MatchRoutingData matchRoutingData = new MatchRoutingData()
            {
                GameServerIp = "someIp",
                GameServerPort = 8918981
            };
            var account = AccountFactory.CreateSimpleAccount();
            await dbContext.Accounts.AddAsync(account);
            await dbContext.SaveChangesAsync();
            var playersBattleInfo = PlayersQueueInfoFactory.CreateSinglePlayer(account);
            
            //Act
            Match match = await matchDbWriterService.WriteMatchDataToDb(matchRoutingData, playersBattleInfo);

            //Assert
            Assert.IsNotNull(match);
            Assert.AreEqual(matchRoutingData.GameServerIp, match.GameServerIp);
            Assert.AreEqual(matchRoutingData.GameServerPort, match.GameServerUdpPort);
        }
    }
}