using System.Collections.Generic;
using System.Linq;
using AmoebaGameMatcherServer.Services;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//TODO сервисы слишком большие

namespace MatchmakerTest
{
    [TestClass]
    public class BattleCreatingTests
    {
        /// <summary>
        /// Должно запустить один матч без ботов.
        /// Создаёт базу с 10-ю аккунтами. Аккаунты добавляются в очередь. Заупчкается сбор матча.
        /// Аккаунты достюются из очреди. И переходят в состояние "В бою". Информация про бой записывается в БД.
        /// </summary>
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
            var matchDataDbWriterService = new MatchDataDbWriterService(dbContext);
            BattleRoyaleMatchCreatorService battleRoyaleMatchCreatorService = new BattleRoyaleMatchCreatorService(
                battleRoyaleMatchPackerService, gameServerNegotiatorServiceStub, matchmakerDichService,
                battleRoyaleUnfinishedMatchesSingletonService, sukaService, matchDataDbWriterService);

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

            //Игроки были извлечены из очереди
            int numberOfPlayersInQueue=battleRoyaleQueueSingletonService.GetNumberOfPlayersInQueue();
            Assert.AreEqual(0, numberOfPlayersInQueue);
            
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
        
        /// <summary>
        /// Должно запустить много матчей без ботов.
        /// Создаёт базу с 500 аккунтами. Аккаунты добавляются в очередь. Заупчкается создание матчей.
        /// Аккаунты достюются из очереди и переходят в состояние "В бою". Информация про бой записывается в БД.
        /// </summary>
        [TestMethod]
        public void Test3()
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
            var matchDataDbWriterService = new MatchDataDbWriterService(dbContext);
            BattleRoyaleMatchCreatorService battleRoyaleMatchCreatorService = new BattleRoyaleMatchCreatorService(
                battleRoyaleMatchPackerService, gameServerNegotiatorServiceStub, matchmakerDichService,
                battleRoyaleUnfinishedMatchesSingletonService, sukaService, matchDataDbWriterService);

            var playerTimeoutController= new PlayerTimeoutManagerService(battleRoyaleQueueSingletonService);
            MatchCreationInitiator matchCreationInitiator =
                new MatchCreationInitiator(battleRoyaleMatchCreatorService, playerTimeoutController);

            int countOfAccountsInDb = 1000;
            
            //Создать новые аккаунты
            for (int i = 1; i <= countOfAccountsInDb; i++)
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
            
            int numberOfPlayersInMatch = countOfAccountsInDb/2;

            //Act
            //Добавить половину игроков в очередь
            foreach (var account in dbContext.Accounts.Take(numberOfPlayersInMatch))
            {
                bool success1 = queueExtenderService.TryEnqueuePlayer(account.ServiceId, account.Warships.First().Id)
                    .Result;
                if (!success1)
                {
                    Assert.Fail();
                }
            }

            //Запустить сборку матчей
            matchCreationInitiator.TryCreateBattleRoyaleMatch().Wait();

            //Assert
            
            //Игроки были извлечены из очереди
            int numberOfPlayersInQueue=battleRoyaleQueueSingletonService.GetNumberOfPlayersInQueue();
            Assert.AreEqual(0, numberOfPlayersInQueue);

            //В базе появилось 500/10 новых матчей
            int matchesCount = dbContext.Matches.Count();
            Assert.AreEqual(50, matchesCount);

            //В базе появилась информация про 500 игроков в бою
            int playerBattleInfoCount = dbContext.PlayerMatchResults.Count();
            Assert.AreEqual(500, playerBattleInfoCount);
        }
        
        /// <summary>
        /// Должно запустить один матч с ботами.
        /// Создаёт базу с аккунтами. В очередь добавляется меньше 10 аккаунтов. Запускачетя создание матчей.
        /// Список игроков дополняется ботами. Аккаунты достюются из очереди и переходят в состояние "В бою".
        /// Информация про бой записывается в БД.
        /// </summary>
        [TestMethod]
        public void Test4()
        {
            //Arrange
            var battleRoyaleQueueSingletonService = new BattleRoyaleQueueSingletonService();
            var battleRoyaleMatchPackerService = new BattleRoyaleMatchPackerService(battleRoyaleQueueSingletonService);
            var gameServersManagerService = new GameServersManagerService();
            var matchmakerDichService = new MatchmakerDichService(gameServersManagerService);
            var battleRoyaleUnfinishedMatchesSingletonService = new BattleRoyaleUnfinishedMatchesSingletonService();
            var gameServerNegotiatorServiceStub = new GameServerNegotiatorServiceStub();
            var dbContext = InMemoryDatabaseFactory.Create();
            var warshipValidatorService = new WarshipValidatorService(dbContext);
            var queueExtenderService = 
                new QueueExtenderService(battleRoyaleQueueSingletonService, warshipValidatorService);

            var sukaService = new QueueHelperSukaService(battleRoyaleQueueSingletonService);
            var matchDataDbWriterService = new MatchDataDbWriterService(dbContext);
            BattleRoyaleMatchCreatorService battleRoyaleMatchCreatorService = new BattleRoyaleMatchCreatorService(
                battleRoyaleMatchPackerService, gameServerNegotiatorServiceStub, matchmakerDichService,
                battleRoyaleUnfinishedMatchesSingletonService, sukaService, matchDataDbWriterService);

            var playerTimeoutManager = new PlayerTimeoutManagerServiceStub();
            var matchCreationInitiator =
                new MatchCreationInitiator(battleRoyaleMatchCreatorService, playerTimeoutManager);

            int countOfAccountsInDb = 50;
            
            //Создать новые аккаунты
            for (int i = 1; i <= countOfAccountsInDb; i++)
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
            
            //Act
            int countOfPlayersInQueue = 5;
            //Добавить игроков в очередь
            foreach (var account in dbContext.Accounts.Take(countOfPlayersInQueue))
            {
                bool success1 = queueExtenderService.TryEnqueuePlayer(account.ServiceId, account.Warships.First().Id)
                    .Result;
                if (!success1)
                {
                    Assert.Fail();
                }
            }

            //Запустить сборку матчей
            bool matchWasCreated = matchCreationInitiator.TryCreateBattleRoyaleMatch().Result;

            //Assert
            //Матч был создан
            Assert.IsTrue(matchWasCreated); 
            
            //Игроки были извлечены из очереди
            int numberOfPlayersInQueue=battleRoyaleQueueSingletonService.GetNumberOfPlayersInQueue();
            Assert.AreEqual(0, numberOfPlayersInQueue);

            //В матч зарегистрирован в БД
            int matchesCount = dbContext.Matches.Count();
            Assert.AreEqual(1, matchesCount);

            //В БД появилась информация про игроков в бою
            int playerBattleInfoCount = dbContext.PlayerMatchResults.Count();
            Assert.AreEqual(countOfPlayersInQueue, playerBattleInfoCount);
        }
    }
}