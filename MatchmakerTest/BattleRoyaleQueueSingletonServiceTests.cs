using System;
using System.Linq;
using AmoebaGameMatcherServer.Services;
using DataLayer.Tables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkLibrary.NetworkLibrary.Http;

//Бесполезные тесты

namespace MatchmakerTest
{
    [TestClass]
    public class BattleRoyaleQueueSingletonServiceTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
            string str1 = "a";

            //Act    
            bool success1 = battleRoyaleQueue.TryEnqueuePlayer(str1, new Warship());
            bool success2 = battleRoyaleQueue.TryEnqueuePlayer(str1, new Warship());

            //Assert
            Assert.IsTrue(success1);
            Assert.IsFalse(success2);
        }
        
        [TestMethod]
        public void TestMethod2()
        {
            //Arrange
            BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
            string str1 = "a";
            string str2 = "a";

            //Act    
            bool success1 = battleRoyaleQueue.TryEnqueuePlayer(str1, new Warship());
            bool success2 = battleRoyaleQueue.TryEnqueuePlayer(str2, new Warship());

            //Assert
            Assert.IsTrue(success1);
            Assert.IsFalse(success2);
        }
        
        [TestMethod]
        public void TestMethod3()
        {
            //Arrange
            BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
            string str1 = "a";
            string str2 = "b";

            //Act    
            battleRoyaleQueue.TryEnqueuePlayer(str1, new Warship());
            battleRoyaleQueue.TryEnqueuePlayer(str2, new Warship());
            int count = battleRoyaleQueue.GetNumberOfPlayersInQueue();
            
            //Assert
            Assert.AreEqual(count, 2);
        }
        
        [TestMethod]
        public void TestMethod4()
        {
            //Arrange
            BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
            string str1 = "a";

            //Act    
            battleRoyaleQueue.TryEnqueuePlayer(str1, new Warship());
            bool success = battleRoyaleQueue.TryRemovePlayerFromQueue(str1);
            
            //Assert
            Assert.IsTrue(success);
        }
        
        [TestMethod]
        public void TestMethod5()
        {
            //Arrange
            BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
            string str1 = "a";
            string str2 = "b";

            //Act    
            battleRoyaleQueue.TryEnqueuePlayer(str1, new Warship());
            bool success = battleRoyaleQueue.TryRemovePlayerFromQueue(str2);
            
            //Assert
            Assert.IsFalse(success);
        }
        
        [TestMethod]
        public void TestMethod6()
        {
            //Arrange
            BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
            string str1 = "a";

            //Act    
            battleRoyaleQueue.TryEnqueuePlayer(str1, new Warship());
            bool success = battleRoyaleQueue.IsPlayerInQueue(str1);
            
            //Assert
            Assert.IsTrue(success);
        }
        
        [TestMethod]
        public void TestMethod7()
        {
            //Arrange
            BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
            string str1 = "a";
            string str2 = "b";
            
            //Act    
            battleRoyaleQueue.TryEnqueuePlayer(str1, new Warship());
            bool success = battleRoyaleQueue.IsPlayerInQueue(str2);
            
            //Assert
            Assert.IsFalse(success);
        }
        
        [TestMethod]
        public void TestMethod8()
        {
            //Arrange
            BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
            string str1 = "a";

            //Act    
            battleRoyaleQueue.TryEnqueuePlayer(str1, new Warship());
            var playersInfo= battleRoyaleQueue.TakeHead(5);
            
            //Assert
            Assert.AreEqual(1, playersInfo.Count);
        }
        
        [TestMethod]
        public void TestMethod9()
        {
            //Arrange
            BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
            string str1 = "a";
            string str2 = "b";
            string str3 = "c";
            string str4 = "d";
            string str5 = "e";

            //Act    
            battleRoyaleQueue.TryEnqueuePlayer(str1, new Warship());
            battleRoyaleQueue.TryEnqueuePlayer(str2, new Warship());
            battleRoyaleQueue.TryEnqueuePlayer(str3, new Warship());
            battleRoyaleQueue.TryEnqueuePlayer(str4, new Warship());
            battleRoyaleQueue.TryEnqueuePlayer(str5, new Warship());
            var playersInfo= battleRoyaleQueue.TakeHead(3);
            
            //Assert
            Assert.AreEqual(3, playersInfo.Count);
            Assert.AreEqual(str1, playersInfo[0].PlayerServiceId);
            Assert.AreEqual(str2, playersInfo[1].PlayerServiceId);
            Assert.AreEqual(str3, playersInfo[2].PlayerServiceId);
        }
        
        [TestMethod]
        public void TestMethod10()
        {
            //Arrange
            BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
            string str1 = "a";
            string str2 = "b";

            //Act    
            battleRoyaleQueue.TryEnqueuePlayer(str1, new Warship());
            battleRoyaleQueue.TryEnqueuePlayer(str2, new Warship());
            battleRoyaleQueue.TryRemovePlayerFromQueue(str1);
            int number = battleRoyaleQueue.GetNumberOfPlayersInQueue();
            
            //Assert
            Assert.AreEqual(1, number);
        }
        
        [TestMethod]
        public void TestMethod11()
        {
            //Arrange
            BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
            string str1 = "a";
            string str2 = "b";

            //Act    
            battleRoyaleQueue.TryEnqueuePlayer(str1, new Warship());
            battleRoyaleQueue.TryEnqueuePlayer(str2, new Warship());
            DateTime? dateTime = battleRoyaleQueue.GetOldestRequestTime();
            var playerInfo = battleRoyaleQueue.TakeHead(1);

            //Assert
            Assert.IsNotNull(dateTime);
            Assert.AreEqual(playerInfo.Single().DictionaryEntryTime, dateTime);
        }
    }
}