using System;
using System.Linq;
using AmoebaGameMatcherServer.Services;
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
            bool success1 = battleRoyaleQueue.TryEnqueuePlayer(str1, new WarshipCopy());
            bool success2 = battleRoyaleQueue.TryEnqueuePlayer(str1, new WarshipCopy());

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
            bool success1 = battleRoyaleQueue.TryEnqueuePlayer(str1, new WarshipCopy());
            bool success2 = battleRoyaleQueue.TryEnqueuePlayer(str2, new WarshipCopy());

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
            battleRoyaleQueue.TryEnqueuePlayer(str1, new WarshipCopy());
            battleRoyaleQueue.TryEnqueuePlayer(str2, new WarshipCopy());
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
            battleRoyaleQueue.TryEnqueuePlayer(str1, new WarshipCopy());
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
            battleRoyaleQueue.TryEnqueuePlayer(str1, new WarshipCopy());
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
            battleRoyaleQueue.TryEnqueuePlayer(str1, new WarshipCopy());
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
            battleRoyaleQueue.TryEnqueuePlayer(str1, new WarshipCopy());
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
            battleRoyaleQueue.TryEnqueuePlayer(str1, new WarshipCopy());
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
            battleRoyaleQueue.TryEnqueuePlayer(str1, new WarshipCopy());
            battleRoyaleQueue.TryEnqueuePlayer(str2, new WarshipCopy());
            battleRoyaleQueue.TryEnqueuePlayer(str3, new WarshipCopy());
            battleRoyaleQueue.TryEnqueuePlayer(str4, new WarshipCopy());
            battleRoyaleQueue.TryEnqueuePlayer(str5, new WarshipCopy());
            var playersInfo= battleRoyaleQueue.TakeHead(3);
            
            //Assert
            Assert.AreEqual(3, playersInfo.Count);
            Assert.AreEqual(str1, playersInfo[0].PlayerId);
            Assert.AreEqual(str2, playersInfo[1].PlayerId);
            Assert.AreEqual(str3, playersInfo[2].PlayerId);
        }
        
        [TestMethod]
        public void TestMethod10()
        {
            //Arrange
            BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
            string str1 = "a";
            string str2 = "b";

            //Act    
            battleRoyaleQueue.TryEnqueuePlayer(str1, new WarshipCopy());
            battleRoyaleQueue.TryEnqueuePlayer(str2, new WarshipCopy());
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
            battleRoyaleQueue.TryEnqueuePlayer(str1, new WarshipCopy());
            battleRoyaleQueue.TryEnqueuePlayer(str2, new WarshipCopy());
            DateTime? dateTime = battleRoyaleQueue.GetOldestRequestTime();
            var playerInfo = battleRoyaleQueue.TakeHead(1);

            //Assert
            Assert.IsNotNull(dateTime);
            Assert.AreEqual(playerInfo.Single().DictionaryEntryTime, dateTime);
        }
    }
}