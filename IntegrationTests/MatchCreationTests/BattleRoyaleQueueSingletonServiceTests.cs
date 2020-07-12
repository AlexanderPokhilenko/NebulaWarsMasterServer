// using System;
// using AmoebaGameMatcherServer.Services.Queues;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
//
// namespace MatchmakerTest
// {
//     //TODO говно
//     [TestClass]
//     public class BattleRoyaleQueueSingletonServiceTests
//     {
//         /// <summary>
//         /// В очередь нельзя добавить два одинаковых аккаунта
//         /// </summary>
//         [TestMethod]
//         public void TestMethod1()
//         {
//             //Arrange
//             BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
//             string playerServiceId = UniqueStringFactory.Create();
//             MatchEntryRequest queueInfoForPlayer = new MatchEntryRequest(playerServiceId, 0,
//                 null, 0, 0 , CreationDateTime.Now);
//
//             //Act    
//             bool success1 = battleRoyaleQueue.TryEnqueue(queueInfoForPlayer);
//             bool success2 = battleRoyaleQueue.TryEnqueue(queueInfoForPlayer);
//             int countOfPlayersInQueue = battleRoyaleQueue.GetNumberOfPlayers();
//             
//             //Assert
//             Assert.IsTrue(success1);
//             Assert.IsFalse(success2);
//             Assert.AreEqual(1, countOfPlayersInQueue);
//         }
//         
//         
//          
//         /// <summary>
//         /// Если попытаться добавить в очередь два корабля с одним собственнком, то второй корабль не добавится.
//         /// </summary>
//         [TestMethod]
//         public void TestMethod2()
//         {
//             //Arrange
//             BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
//             string playerServiceId = UniqueStringFactory.Create();
//             MatchEntryRequest queueInfoForPlayer1 = new MatchEntryRequest(playerServiceId, 0,
//                 null, 0, 0 , CreationDateTime.Now);
//             MatchEntryRequest queueInfoForPlayer2 = new MatchEntryRequest(playerServiceId, 0,
//                 null, 0, 1 , CreationDateTime.Now);
//
//             //Act    
//             bool success1 = battleRoyaleQueue.TryEnqueue(queueInfoForPlayer1);
//             bool success2 = battleRoyaleQueue.TryEnqueue(queueInfoForPlayer2);
//
//             
//             //Assert
//             Assert.IsTrue(success1);
//             Assert.IsFalse(success2);
//             int countOfPlayersInQueue = battleRoyaleQueue.GetNumberOfPlayers();
//             Assert.AreEqual(1, countOfPlayersInQueue);
//         }
//         
//         
//         /// <summary>
//         /// Если успешно добавить два аккаунта в очередь, то кол-во элементов в очереди будет равным 2.
//         /// </summary>
//         [TestMethod]
//         public void TestMethod3()
//         {
//             //Arrange
//             BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
//             string playerServiceId1 = UniqueStringFactory.Create();
//             string playerServiceId2 = UniqueStringFactory.Create();
//             
//             MatchEntryRequest queueInfoForPlayer1 = new MatchEntryRequest(playerServiceId1, 0,
//                 null, 0, 0 , CreationDateTime.Now);
//             MatchEntryRequest queueInfoForPlayer2 = new MatchEntryRequest(playerServiceId2, 0,
//                 null, 0, 1 , CreationDateTime.Now);
//
//             //Act    
//             bool success1 = battleRoyaleQueue.TryEnqueue(queueInfoForPlayer1);
//             bool success2 = battleRoyaleQueue.TryEnqueue(queueInfoForPlayer2);
//             
//             //Assert
//             Assert.IsTrue(success1);
//             Assert.IsTrue(success2);
//             int numberOfPlayersInQueue = battleRoyaleQueue.GetNumberOfPlayers();
//             Assert.AreEqual(2, numberOfPlayersInQueue);
//             
//         }
//         
//         // /// <summary>
//         // /// Аккаунт можно добавлять в очередь и исключать из неё
//         // /// </summary>
//         // [TestMethod]
//         // public void TestMethod4()
//         // {
//         //     //Arrange
//         //     BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
//         //     Warship warship1 = new Warship
//         //     {
//         //         Account = new Account
//         //         {
//         //             ServiceId = "a"
//         //         }
//         //     };
//         //
//         //     //Act    
//         //     battleRoyaleQueue.TryEnqueuePlayerAsync(warship1.Account.ServiceId, warship1);
//         //     bool success = battleRoyaleQueue.TryRemove(str1);
//         //     
//         //     //Assert
//         //     Assert.IsTrue(success);
//         // }
//         //
//         // [TestMethod]
//         // public void TestMethod5()
//         // {
//         //     //Arrange
//         //     BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
//         //     string str1 = "a";
//         //     string str2 = "b";
//         //
//         //     //Act    
//         //     battleRoyaleQueue.TryEnqueuePlayerAsync(str1, new Warship());
//         //     bool success = battleRoyaleQueue.TryRemove(str2);
//         //     
//         //     //Assert
//         //     Assert.IsFalse(success);
//         // }
//         //
//         // [TestMethod]
//         // public void TestMethod6()
//         // {
//         //     //Arrange
//         //     BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
//         //     string str1 = "a";
//         //
//         //     //Act    
//         //     battleRoyaleQueue.TryEnqueuePlayerAsync(str1, new Warship());
//         //     bool success = battleRoyaleQueue.Contains(str1);
//         //     
//         //     //Assert
//         //     Assert.IsTrue(success);
//         // }
//         //
//         // [TestMethod]
//         // public void TestMethod7()
//         // {
//         //     //Arrange
//         //     BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
//         //     string str1 = "a";
//         //     string str2 = "b";
//         //     
//         //     //Act    
//         //     battleRoyaleQueue.TryEnqueuePlayerAsync(str1, new Warship());
//         //     bool success = battleRoyaleQueue.Contains(str2);
//         //     
//         //     //Assert
//         //     Assert.IsFalse(success);
//         // }
//         //
//         // [TestMethod]
//         // public void TestMethod8()
//         // {
//         //     //Arrange
//         //     BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
//         //     string str1 = "a";
//         //
//         //     //Act    
//         //     battleRoyaleQueue.TryEnqueuePlayerAsync(str1, new Warship());
//         //     var playersInfo= battleRoyaleQueue.TakeMatchEntryRequests(5);
//         //     
//         //     //Assert
//         //     Assert.AreEqual(1, playersInfo.Count);
//         // }
//         //
//         // [TestMethod]
//         // public void TestMethod9()
//         // {
//         //     //Arrange
//         //     BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
//         //     string str1 = "a";
//         //     string str2 = "b";
//         //     string str3 = "c";
//         //     string str4 = "d";
//         //     string str5 = "e";
//         //
//         //     //Act    
//         //     battleRoyaleQueue.TryEnqueuePlayerAsync(str1, new Warship());
//         //     battleRoyaleQueue.TryEnqueuePlayerAsync(str2, new Warship());
//         //     battleRoyaleQueue.TryEnqueuePlayerAsync(str3, new Warship());
//         //     battleRoyaleQueue.TryEnqueuePlayerAsync(str4, new Warship());
//         //     battleRoyaleQueue.TryEnqueuePlayerAsync(str5, new Warship());
//         //     var playersInfo= battleRoyaleQueue.TakeMatchEntryRequests(3);
//         //     
//         //     //Assert
//         //     Assert.AreEqual(3, playersInfo.Count);
//         //     Assert.AreEqual(str1, playersInfo[0].PlayerServiceId);
//         //     Assert.AreEqual(str2, playersInfo[1].PlayerServiceId);
//         //     Assert.AreEqual(str3, playersInfo[2].PlayerServiceId);
//         // }
//         //
//         // [TestMethod]
//         // public void TestMethod10()
//         // {
//         //     //Arrange
//         //     BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
//         //     string str1 = "a";
//         //     string str2 = "b";
//         //
//         //     //Act    
//         //     battleRoyaleQueue.TryEnqueuePlayerAsync(str1, new Warship());
//         //     battleRoyaleQueue.TryEnqueuePlayerAsync(str2, new Warship());
//         //     battleRoyaleQueue.TryRemove(str1);
//         //     int number = battleRoyaleQueue.GetNumberOfPlayers();
//         //     
//         //     //Assert
//         //     Assert.AreEqual(1, number);
//         // }
//         //
//         // [TestMethod]
//         // public void TestMethod11()
//         // {
//         //     //Arrange
//         //     BattleRoyaleQueueSingletonService battleRoyaleQueue= new BattleRoyaleQueueSingletonService();
//         //     string str1 = "a";
//         //     string str2 = "b";
//         //
//         //     //Act    
//         //     battleRoyaleQueue.TryEnqueuePlayerAsync(str1, new Warship());
//         //     battleRoyaleQueue.TryEnqueuePlayerAsync(str2, new Warship());
//         //     CreationDateTime? dateTime = battleRoyaleQueue.GetOldestRequestTime();
//         //     var playerInfo = battleRoyaleQueue.TakeMatchEntryRequests(1);
//         //
//         //     //Assert
//         //     Assert.IsNotNull(dateTime);
//         //     Assert.AreEqual(playerInfo.Single().DictionaryEntryTime, dateTime);
//         // }
//     }
//
//     public class UniqueStringFactory
//     {
//         public static string Create()
//         {
//             return Guid.NewGuid().ToString();
//         }
//     }
// }