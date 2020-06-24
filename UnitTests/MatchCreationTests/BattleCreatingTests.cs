using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services;
using AmoebaGameMatcherServer.Services.GameServerNegotiation;
using AmoebaGameMatcherServer.Services.MatchCreation;
using AmoebaGameMatcherServer.Services.MatchCreationInitiation;
using AmoebaGameMatcherServer.Services.PlayerQueueing;
using AmoebaGameMatcherServer.Services.Queues;
using AmoebaGameMatcherServer.Utils;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//TODO длинна теста слишком велика
//TODO нормально назвать тесты

namespace MatchmakerTest
{
    [TestClass]
    public class BattleCreatingTests
    {
        /// <summary>
        /// Должно запустить один матч без ботов.
        /// Создаёт базу с 10-ю аккунтами. Аккаунты добавляются в очередь. Запускается сбор матча.
        /// Аккаунты достаются из очереди и переходят в состояние "В бою". Информация про бой записывается в БД.
        /// </summary>
        [TestMethod]
        public async Task Test2()
        {
            //Arrange
            var battleRoyaleQueueSingletonService = new BattleRoyaleQueueSingletonService();
            var battleRoyaleMatchPackerService = new BattleRoyaleMatchPackerService(battleRoyaleQueueSingletonService);
            var gameServersManagerService = new GameServersRoutingDataService();
            var matchmakerDichService = new MatchRoutingDataService(gameServersManagerService);
            var battleRoyaleUnfinishedMatchesSingletonService = new BattleRoyaleUnfinishedMatchesSingletonService();
            IGameServerNegotiatorService gameServerNegotiatorServiceStub = new GameServerNegotiatorServiceStub();
            IDbContextFactory dbContextFactory = new InMemoryDbContextFactory(nameof(BattleCreatingTests)+nameof(Test2));
            var dbContext = dbContextFactory.Create();
            
            QueueExtenderService queueExtenderService = 
                new QueueExtenderService(battleRoyaleQueueSingletonService, dbContext);
            var matchDataDbWriterService = new MatchDbWriterService(dbContextFactory);
            
            BattleRoyaleMatchCreatorService battleRoyaleMatchCreatorService = new BattleRoyaleMatchCreatorService(
                battleRoyaleMatchPackerService, gameServerNegotiatorServiceStub, matchmakerDichService,
                battleRoyaleUnfinishedMatchesSingletonService, battleRoyaleQueueSingletonService, 
                matchDataDbWriterService);

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
                            WarshipTypeId = WarshipTypeEnum.Hare
                        }
                    }
                };
                await dbContext.Accounts.AddAsync(account);
            }
            await dbContext.SaveChangesAsync();
            int numberOfPlayersInMatch = 10;

            //Act
            //Добавить игроков в очередь
            foreach (var account in dbContext.Accounts)
            {
                bool success1 = await queueExtenderService.TryEnqueuePlayerAsync(account.ServiceId, account.Warships.First().Id);
                if (!success1)
                {
                    Assert.Fail();
                }
            }

            //Запустить сборку матчей
            var result = await battleRoyaleMatchCreatorService
                .TryCreateMatch(numberOfPlayersInMatch, false);

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
                .Include(match1 => match1.MatchResultForPlayers)
                .SingleOrDefault(match1 => match1.Id == result.MatchId);
            Assert.IsNotNull(match);

            //Есть информация про игроков
            Assert.IsNotNull(match.MatchResultForPlayers);

            //Количество игроков в БД правильное
            List<int> playerInMatchIds = match.MatchResultForPlayers.Select(matchResult => matchResult.Warship.AccountId).ToList();
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
        public async Task Test3()
        {
            //Arrange
            var battleRoyaleQueueSingletonService = new BattleRoyaleQueueSingletonService();
            var battleRoyaleMatchPackerService = new BattleRoyaleMatchPackerService(battleRoyaleQueueSingletonService);
            var gameServersManagerService = new GameServersRoutingDataService();
            var matchmakerDichService = new MatchRoutingDataService(gameServersManagerService);
            var battleRoyaleUnfinishedMatchesSingletonService = new BattleRoyaleUnfinishedMatchesSingletonService();
            IGameServerNegotiatorService gameServerNegotiatorServiceStub = new GameServerNegotiatorServiceStub();
            IDbContextFactory dbContextFactory = new InMemoryDbContextFactory(nameof(BattleCreatingTests)+nameof(Test3));
            var dbContext = dbContextFactory.Create();
            
            QueueExtenderService queueExtenderService = 
                new QueueExtenderService(battleRoyaleQueueSingletonService, dbContext);
            var matchDataDbWriterService = new MatchDbWriterService(dbContextFactory);
            BattleRoyaleMatchCreatorService battleRoyaleMatchCreatorService = new BattleRoyaleMatchCreatorService(
                battleRoyaleMatchPackerService, gameServerNegotiatorServiceStub, matchmakerDichService,
                battleRoyaleUnfinishedMatchesSingletonService, battleRoyaleQueueSingletonService, matchDataDbWriterService);

            var playerTimeoutController= new PlayerTimeoutManagerService(battleRoyaleQueueSingletonService);
            MatchCreationInitiator matchCreationInitiator =
                new MatchCreationInitiator(battleRoyaleMatchCreatorService, playerTimeoutController);


            int countOfAccounts = 250;
            //Создать новые аккаунты
            for (int i = 1; i <= countOfAccounts; i++)
            {
                Account account = new Account
                {
                    ServiceId = i.ToString(),
                    Username = i.ToString(),
                    Warships = new List<Warship>
                    {
                        new Warship
                        {
                            WarshipTypeId = WarshipTypeEnum.Hare
                        }
                    }
                };
                await dbContext.Accounts.AddAsync(account);
            }
            await dbContext.SaveChangesAsync();
            
            //Act
            //Добавить половину игроков в очередь
            foreach (var account in dbContext.Accounts.Take(countOfAccounts))
            {
                bool success1 = await queueExtenderService.TryEnqueuePlayerAsync(account.ServiceId, account.Warships.First().Id);
                if (!success1)
                {
                    Assert.Fail();
                }
            }

            //Запустить сборку матчей
            await matchCreationInitiator.TryCreateBattleRoyaleMatch();

            //Assert
            
            //Игроки были извлечены из очереди
            int numberOfPlayersInQueue=battleRoyaleQueueSingletonService.GetNumberOfPlayersInQueue();
            Assert.AreEqual(0, numberOfPlayersInQueue);

            //В базе появилось n новых матчей
            int matchesCount = dbContext.Matches.Count();
            int expectedNumberOfMatches = countOfAccounts / Globals.NumbersOfPlayersInBattleRoyaleMatch;
            Assert.AreEqual(expectedNumberOfMatches, matchesCount);

            //В базе появилась информация про m игроков в бою
            int playerBattleInfoCount = dbContext.MatchResultForPlayers.Count();
            Assert.AreEqual(countOfAccounts, playerBattleInfoCount);
        }
        
        /// <summary>
        /// Должно запустить один матч с ботами.
        /// Создаёт базу с аккунтами. В очередь добавляется меньше 10 аккаунтов. Запускачетя создание матчей.
        /// Список игроков дополняется ботами. Аккаунты достюются из очереди и переходят в состояние "В бою".
        /// Информация про бой записывается в БД.
        /// </summary>
        [TestMethod]
        public async Task Test4()
        {
            //Arrange
            var battleRoyaleQueueSingletonService = new BattleRoyaleQueueSingletonService();
            var battleRoyaleMatchPackerService = new BattleRoyaleMatchPackerService(battleRoyaleQueueSingletonService);
            var gameServersManagerService = new GameServersRoutingDataService();
            var matchmakerDichService = new MatchRoutingDataService(gameServersManagerService);
            var battleRoyaleUnfinishedMatchesSingletonService = new BattleRoyaleUnfinishedMatchesSingletonService();
            var gameServerNegotiatorServiceStub = new GameServerNegotiatorServiceStub();
            IDbContextFactory dbContextFactory = new InMemoryDbContextFactory(nameof(BattleCreatingTests)+nameof(Test4));
            var dbContext = dbContextFactory.Create();
            
            var queueExtenderService = 
                new QueueExtenderService(battleRoyaleQueueSingletonService, dbContext);
            
            var matchDataDbWriterService = new MatchDbWriterService(dbContextFactory);
            BattleRoyaleMatchCreatorService battleRoyaleMatchCreatorService = new BattleRoyaleMatchCreatorService(
                battleRoyaleMatchPackerService, gameServerNegotiatorServiceStub, matchmakerDichService,
                battleRoyaleUnfinishedMatchesSingletonService, battleRoyaleQueueSingletonService, matchDataDbWriterService);

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
                            WarshipTypeId = WarshipTypeEnum.Hare
                        }
                    }
                };
                await dbContext.Accounts.AddAsync(account);
            }
            await dbContext.SaveChangesAsync();
            
            //Act
            int countOfPlayersInQueue = 5;
            //Добавить игроков в очередь
            foreach (var account in dbContext.Accounts.Take(countOfPlayersInQueue))
            {
                bool success1 = await queueExtenderService.TryEnqueuePlayerAsync(account.ServiceId, account.Warships.First().Id);
                if (!success1)
                {
                    Assert.Fail();
                }
            }

            //Запустить сборку матчей
            bool matchWasCreated = await matchCreationInitiator.TryCreateBattleRoyaleMatch();

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
            int playerBattleInfoCount = dbContext.MatchResultForPlayers.Count();
            Assert.AreEqual(countOfPlayersInQueue, playerBattleInfoCount);
        }
    }
}