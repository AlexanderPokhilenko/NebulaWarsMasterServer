using System.Collections.Generic;
using System.Linq;
using AmoebaGameMatcherServer.Services;
using DataLayer;
using DataLayer.Tables;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatchmakerTest
{
    [TestClass]
    public class BattleCreatingTests
    {
         [TestMethod]
         public void Test2()
         {
            //Arrange
            var battleRoyaleQueueSingletonService = new BattleRoyaleQueueSingletonService();
            var battleRoyaleMatchPackerService = new BattleRoyaleMatchPackerService(battleRoyaleQueueSingletonService);
            var gameServersManagerService = new GameServersManagerService();
            var matchmakerDichService = new MatchmakerDichService(gameServersManagerService);
            var battleRoyaleUnfinishedMatchesSingletonService = new BattleRoyaleUnfinishedMatchesSingletonService();
            IGameServerNegotiatorService gameServerNegotiatorServiceStub = new GameServerNegotiatorServiceStub();
            var dbContext = InMemoryDatabaseFactory.Create();
            IWarshipValidatorService warshipValidatorService = new WarshipValidatorService(dbContext);
            QueueExtenderService queueExtenderService = 
                new QueueExtenderService(battleRoyaleQueueSingletonService, warshipValidatorService);
            
            QueueHelperSukaService sukaService = new QueueHelperSukaService(battleRoyaleQueueSingletonService);
            BattleRoyaleMatchCreatorService battleRoyaleMatchCreatorService = new BattleRoyaleMatchCreatorService(
                battleRoyaleMatchPackerService,
                dbContext,
                gameServerNegotiatorServiceStub,
                matchmakerDichService,
                battleRoyaleUnfinishedMatchesSingletonService,
                sukaService
                );
            
            int countOfAccountsInDb = 15;
            for (int i = 0; i < countOfAccountsInDb; i++)
            {
                Account account = new Account
                {
                    ServiceId = i.ToString(),
                    Username = i.ToString(),
                    Warships = new List<Warship>
                    {
                        new Warship
                        {
                            WarshipTypeId = 1
                        }
                    }
                };
                dbContext.Accounts.Add(account);
            }
            dbContext.SaveChanges();
            int numberOfPlayers = 10;

            //Act
            //Добавить игроков в очередь
            foreach (var account in dbContext.Accounts)
            {
                bool success1 = queueExtenderService.TryEnqueuePlayer(account.ServiceId, account.Warships.First().Id)
                    .Result;
                if (!success1)
                {
                    Assert.Fail();
                }
            }

            //Запустить сборку матчей
            var (success2, failureReason) = battleRoyaleMatchCreatorService
                .TryCreateMatch(numberOfPlayers, false).Result;
            
            
            //Assert
            Assert.IsTrue(success2);
         }
    }
}