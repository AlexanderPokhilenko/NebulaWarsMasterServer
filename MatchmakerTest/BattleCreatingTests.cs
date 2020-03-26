using AmoebaGameMatcherServer.Services;
using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatchmakerTest
{
    [TestClass]
    public class BattleCreatingTests
    {
        [TestMethod]
        public void Test1()
        {
            //Arrange
            ApplicationDbContext dbContext = InMemoryDatabaseFactory.Create();
            var gameServerNegotiatorService = new GameServerNegotiatorService();
            var queueSingletonService = new BattleRoyaleQueueSingletonService();
            var battleRoyaleMatchPackerService = new BattleRoyaleMatchPackerService(queueSingletonService);
            
            var battleRoyaleMatchCreatorService =
                new BattleRoyaleMatchCreatorService(battleRoyaleMatchPackerService, dbContext, gameServerNegotiatorService);
            
            //Act
            
            
            //Assert
            
            
        }
        
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Test2()
        {
            //Arrange
            var queueSingletonService = new BattleRoyaleQueueSingletonService();
            var battleRoyaleMatchPackerService = new BattleRoyaleMatchPackerService(queueSingletonService);
            
            //Act
            
            
            //Assert
        }
    }
}