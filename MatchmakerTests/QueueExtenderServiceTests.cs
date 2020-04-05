using System.Collections.Generic;
using System.Linq;
using AmoebaGameMatcherServer.Services;
using AmoebaGameMatcherServer.Utils;
using DataLayer;
using DataLayer.Tables;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatchmakerTest
{
    [TestClass]
    public class QueueExtenderServiceTests
    {
        /// <summary>
        /// При добавлении игроков они все появятся в очереди после валидации данных.
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            BattleRoyaleQueueSingletonService battleRoyaleQueueSingletonService = new BattleRoyaleQueueSingletonService();
            WarshipValidatorServiceStub warshipValidatorServiceStub = new WarshipValidatorServiceStub();
            QueueExtenderService queueExtenderService = new QueueExtenderService(battleRoyaleQueueSingletonService, 
                warshipValidatorServiceStub);

            //Act    
            for (int i = 0; i < Globals.NumbersOfPlayersInBattleRoyaleMatch; i++)
            {
                bool success = queueExtenderService.TryEnqueuePlayer(i.ToString(), i).Result;
                if (!success)
                {
                    Assert.Fail();
                }
            }
            int numberOfPlayersInQueue = battleRoyaleQueueSingletonService.GetNumberOfPlayersInQueue();
            
            //Assert
            Assert.AreEqual(Globals.NumbersOfPlayersInBattleRoyaleMatch, numberOfPlayersInQueue);
        }
        
        /// <summary>
        /// Боевой сервис валидации данных пропускает нормальные данные
        /// </summary>
        [TestMethod]
        public void TestMethod2()
        {
            //Arrange
            ApplicationDbContext dbContext = new InMemoryDbContextFactory(nameof(QueueExtenderServiceTests)).Create();
            WarshipValidatorService warshipValidatorService = new WarshipValidatorService(dbContext);
            Account account = new Account
            {
                ServiceId = "ass",
                Warships = new List<Warship>
                {
                    new Warship
                    {
                        //При инициализации базы создаются стандартные типы кораблей
                        WarshipTypeId = 1
                    }
                }
            };
            dbContext.Accounts.Add(account);
            dbContext.SaveChanges();
            
            //Act
            var result = warshipValidatorService
                .GetWarshipById(account.ServiceId, account.Warships.Single().Id).Result;
            
            //Assert
            Assert.IsTrue(result.success);
        }
        
        /// <summary>
        /// Боевой сервис валидации данных не пропускает плохие данные
        /// </summary>
        [TestMethod]
        public void TestMethod3()
        {
            //Arrange
            ApplicationDbContext dbContext = new InMemoryDbContextFactory(nameof(QueueExtenderServiceTests)).Create();
            WarshipValidatorService warshipValidatorService = new WarshipValidatorService(dbContext);
            
            //Act
            var result = warshipValidatorService.GetWarshipById("ss", 45).Result;

            //Assert
            Assert.IsFalse(result.success);
            
        }
        
        /// <summary>
        /// Боевой сервис валидации данных не пропускает плохие данные
        /// </summary>
        [TestMethod]
        public void TestMethod4()
        {
            //Arrange
            ApplicationDbContext dbContext = new InMemoryDbContextFactory(nameof(QueueExtenderServiceTests)).Create();
            WarshipValidatorService warshipValidatorService = new WarshipValidatorService(dbContext);
            Account account1 = new Account
            {
                ServiceId = "www",
                Warships = new List<Warship>
                {
                    new Warship
                    {
                        //При инициализации базы создаются стандартные типы кораблей
                        WarshipTypeId = 1
                    }
                }
            };
            Account account2 = new Account
            {
                ServiceId = "qqq",
                Warships = new List<Warship>
                {
                    new Warship
                    {
                        //При инициализации базы создаются стандартные типы кораблей
                        WarshipTypeId = 1
                    }
                }
            };
            dbContext.Accounts.Add(account1);
            dbContext.Accounts.Add(account2);
            dbContext.SaveChanges();
            
            //Act
            var result = warshipValidatorService
                .GetWarshipById(account1.ServiceId, account2.Warships.Single().Id).Result;

            //Assert
            Assert.IsFalse(result.success);
        }
        
    }
}