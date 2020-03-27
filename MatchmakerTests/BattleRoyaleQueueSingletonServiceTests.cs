using System;
using System.Linq;
using AmoebaGameMatcherServer.Services;
using DataLayer.Tables;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatchmakerTest
{
    [TestClass]
    public class BattleRoyaleQueueSingletonServiceTests
    {
        /// <summary>
        /// В очередь нельзя добавить два одинаковых аккаунта
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
            Warship warship1 = new Warship
            {
                Account = new Account
                {
                    ServiceId = "a"
                }
            };

            //Act    
            bool success1 = battleRoyaleQueue.TryEnqueuePlayer(warship1.Account.ServiceId, warship1) ;
            bool success2 = battleRoyaleQueue.TryEnqueuePlayer(warship1.Account.ServiceId, warship1) ;
            int countOfPlayersInQueue = battleRoyaleQueue.GetNumberOfPlayersInQueue();
            
            //Assert
            Assert.IsTrue(success1);
            Assert.IsFalse(success2);
            Assert.AreEqual(1, countOfPlayersInQueue);
        }
        
        
         
        /// <summary>
        /// Если попытаться добавить в очередь два корабля с одним собственнком, то второй корабль не добавится.
        /// </summary>
        [TestMethod]
        public void TestMethod2()
        {
            //Arrange
            BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
            string serviceId = "a";
            Warship warship1 = new Warship
            {
                Account = new Account
                {
                    ServiceId = serviceId
                }
            };

            Warship warship2 = new Warship
            {
                Account = new Account
                {
                    ServiceId = string.Copy(serviceId)
                }
            };


            //Act    
            bool success1 = battleRoyaleQueue.TryEnqueuePlayer(warship1.Account.ServiceId, warship1);
            bool success2 = battleRoyaleQueue.TryEnqueuePlayer(warship2.Account.ServiceId, warship2);
            
            int countOfPlayersInQueue = battleRoyaleQueue.GetNumberOfPlayersInQueue();
            
            //Assert
            Assert.AreEqual(1, countOfPlayersInQueue);
        }
        
        
        /// <summary>
        /// Если успешно добавить два аккаунта в очередь, то кол-во элементов в очереди будет равным 2.
        /// </summary>
        [TestMethod]
        public void TestMethod3()
        {
            //Arrange
            BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
            Warship warship1 = new Warship
            {
                Account = new Account
                {
                    ServiceId = "a"
                }
            };
            Warship warship2 = new Warship
            {
                Account = new Account
                {
                    ServiceId = "b"
                }
            };

            //Act    
            battleRoyaleQueue.TryEnqueuePlayer(warship1.Account.ServiceId, warship1);
            battleRoyaleQueue.TryEnqueuePlayer(warship2.Account.ServiceId, warship2);
            int numberOfPlayersInQueue = battleRoyaleQueue.GetNumberOfPlayersInQueue();
            
            //Assert
            Assert.AreEqual(2, numberOfPlayersInQueue);
        }
        
        // /// <summary>
        // /// Аккаунт можно добавлять в очередь и исключать из неё
        // /// </summary>
        // [TestMethod]
        // public void TestMethod4()
        // {
        //     //Arrange
        //     BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
        //     Warship warship1 = new Warship
        //     {
        //         Account = new Account
        //         {
        //             ServiceId = "a"
        //         }
        //     };
        //
        //     //Act    
        //     battleRoyaleQueue.TryEnqueuePlayer(warship1.Account.ServiceId, warship1);
        //     bool success = battleRoyaleQueue.TryRemovePlayerFromQueue(str1);
        //     
        //     //Assert
        //     Assert.IsTrue(success);
        // }
        //
        // [TestMethod]
        // public void TestMethod5()
        // {
        //     //Arrange
        //     BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
        //     string str1 = "a";
        //     string str2 = "b";
        //
        //     //Act    
        //     battleRoyaleQueue.TryEnqueuePlayer(str1, new Warship());
        //     bool success = battleRoyaleQueue.TryRemovePlayerFromQueue(str2);
        //     
        //     //Assert
        //     Assert.IsFalse(success);
        // }
        //
        // [TestMethod]
        // public void TestMethod6()
        // {
        //     //Arrange
        //     BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
        //     string str1 = "a";
        //
        //     //Act    
        //     battleRoyaleQueue.TryEnqueuePlayer(str1, new Warship());
        //     bool success = battleRoyaleQueue.IsPlayerInQueue(str1);
        //     
        //     //Assert
        //     Assert.IsTrue(success);
        // }
        //
        // [TestMethod]
        // public void TestMethod7()
        // {
        //     //Arrange
        //     BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
        //     string str1 = "a";
        //     string str2 = "b";
        //     
        //     //Act    
        //     battleRoyaleQueue.TryEnqueuePlayer(str1, new Warship());
        //     bool success = battleRoyaleQueue.IsPlayerInQueue(str2);
        //     
        //     //Assert
        //     Assert.IsFalse(success);
        // }
        //
        // [TestMethod]
        // public void TestMethod8()
        // {
        //     //Arrange
        //     BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
        //     string str1 = "a";
        //
        //     //Act    
        //     battleRoyaleQueue.TryEnqueuePlayer(str1, new Warship());
        //     var playersInfo= battleRoyaleQueue.TakeHead(5);
        //     
        //     //Assert
        //     Assert.AreEqual(1, playersInfo.Count);
        // }
        //
        // [TestMethod]
        // public void TestMethod9()
        // {
        //     //Arrange
        //     BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
        //     string str1 = "a";
        //     string str2 = "b";
        //     string str3 = "c";
        //     string str4 = "d";
        //     string str5 = "e";
        //
        //     //Act    
        //     battleRoyaleQueue.TryEnqueuePlayer(str1, new Warship());
        //     battleRoyaleQueue.TryEnqueuePlayer(str2, new Warship());
        //     battleRoyaleQueue.TryEnqueuePlayer(str3, new Warship());
        //     battleRoyaleQueue.TryEnqueuePlayer(str4, new Warship());
        //     battleRoyaleQueue.TryEnqueuePlayer(str5, new Warship());
        //     var playersInfo= battleRoyaleQueue.TakeHead(3);
        //     
        //     //Assert
        //     Assert.AreEqual(3, playersInfo.Count);
        //     Assert.AreEqual(str1, playersInfo[0].PlayerServiceId);
        //     Assert.AreEqual(str2, playersInfo[1].PlayerServiceId);
        //     Assert.AreEqual(str3, playersInfo[2].PlayerServiceId);
        // }
        //
        // [TestMethod]
        // public void TestMethod10()
        // {
        //     //Arrange
        //     BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
        //     string str1 = "a";
        //     string str2 = "b";
        //
        //     //Act    
        //     battleRoyaleQueue.TryEnqueuePlayer(str1, new Warship());
        //     battleRoyaleQueue.TryEnqueuePlayer(str2, new Warship());
        //     battleRoyaleQueue.TryRemovePlayerFromQueue(str1);
        //     int number = battleRoyaleQueue.GetNumberOfPlayersInQueue();
        //     
        //     //Assert
        //     Assert.AreEqual(1, number);
        // }
        //
        // [TestMethod]
        // public void TestMethod11()
        // {
        //     //Arrange
        //     BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
        //     string str1 = "a";
        //     string str2 = "b";
        //
        //     //Act    
        //     battleRoyaleQueue.TryEnqueuePlayer(str1, new Warship());
        //     battleRoyaleQueue.TryEnqueuePlayer(str2, new Warship());
        //     DateTime? dateTime = battleRoyaleQueue.GetOldestRequestTime();
        //     var playerInfo = battleRoyaleQueue.TakeHead(1);
        //
        //     //Assert
        //     Assert.IsNotNull(dateTime);
        //     Assert.AreEqual(playerInfo.Single().DictionaryEntryTime, dateTime);
        // }
    }
}