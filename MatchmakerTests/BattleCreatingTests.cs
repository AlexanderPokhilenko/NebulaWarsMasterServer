using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AmoebaGameMatcherServer.Services;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.Utilities;
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
            
            int countOfAccountsInDb = 10;
            //Создать новые аккаунты
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
            int numberOfPlayersInMatch = 10;

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
            var result = battleRoyaleMatchCreatorService
                .TryCreateMatch(numberOfPlayersInMatch, false).Result;
            
            
            //Assert
            
            //Сервис вернул ok
            Assert.IsTrue(result.Success);
            
            //Нет ошибки
            Assert.IsNull(result.FailureReason);
            
            //Вернул matchId
            Assert.IsNotNull(result.MatchId);
            
            //Матч записан в базу
            var match = dbContext.Matches
                .Include(match1 => match1.PlayerMatchResults)
                .SingleOrDefault(match1 => match1.Id == result.MatchId);
            Assert.IsNotNull(match);
            
            //Есть информация про игроков
            Assert.IsNotNull(match.PlayerMatchResults);

            //Количество игроков в БД правильное
            List<int> playerInMatchIds = match.PlayerMatchResults.Select(matchResult => matchResult.AccountId).ToList();
            Assert.AreEqual(numberOfPlayersInMatch, playerInMatchIds.Count);

            //Информация про игроков в бою была записана в БД
            foreach (var account in dbContext.Accounts)
            {
                if (!playerInMatchIds.Contains(account.Id))
                {
                    Assert.Fail("В коллекции игроков в матче не найден игрок с id ="+account.Id);
                }
            }
         }
    }
}