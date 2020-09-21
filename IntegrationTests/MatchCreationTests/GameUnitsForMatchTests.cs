// using System;
// using System.Collections.Generic;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using NetworkLibrary.NetworkLibrary.Http;
//
//
// namespace MatchmakerTest
// {
//     [TestClass]
//     public class GameUnitsForMatchTests
//     {
//         /// <summary>
//         /// Индексатор работает как я ожидаю
//         /// </summary>
//         [TestMethod]
//         public void Test1()
//         {
//             //Arrange
//             GameUnits gameUnits = new GameUnits();
//             List<PlayerInfoForMatch> playerInfoForMatches = new List<PlayerInfoForMatch>();
//             List<BotModel> botInfos = new List<BotModel>();
//             int countOfPlayers = 45;
//             int countOfBots = 12;
//
//             for (int i = 0; i < countOfPlayers; i++)
//             {
//                 PlayerInfoForMatch playerInfoForMatch = new PlayerInfoForMatch()
//                 {
//                     AccountId = i,
//                     IsBot = false,
//                     WarshipName = "dich",
//                     ServiceId = i.ToString(),
//                     TemporaryId = 25,
//                     WarshipPowerLevel = 5
//                 };
//                 playerInfoForMatches.Add(playerInfoForMatch);
//             }
//
//             for (int i = 0; i < countOfBots; i++)
//             {
//                 BotModel botInfo = new BotModel()
//                 {
//                     IsBot = true,
//                     WarshipName = "dich",
//                     TemporaryId = 25,
//                     WarshipPowerLevel = 5
//                 };
//                 botInfos.Add(botInfo);
//             }
//
//             gameUnits.Players = playerInfoForMatches;
//             gameUnits.Bots = botInfos;
//
//             //Act
//             for (int i = 0; i < countOfPlayers; i++)
//             {
//                 if (playerInfoForMatches[i] != gameUnits[i])
//                 {
//                     Assert.Fail();
//                 }
//             }
//
//             for (int i = countOfPlayers; i < countOfPlayers + countOfBots; i++)
//             {
//                 if (botInfos[i - countOfPlayers] != gameUnits[i])
//                 {
//                     Assert.Fail();
//                 }
//             }
//
//             //Assert
//         }
//
//         /// <summary>
//         /// Индексатор работает как я ожидаю
//         /// </summary>
//         [TestMethod]
//         public void Test2()
//         {
//             //Arrange
//             GameUnits gameUnits = new GameUnits()
//             {
//                 Bots = new List<BotModel>
//                 {
//                     new BotModel()
//                     {
//                         IsBot = true,
//                         WarshipName = "dich",
//                         TemporaryId = 25,
//                         WarshipPowerLevel = 5
//                     }
//                 },
//                 Players = new List<PlayerInfoForMatch>
//                 {
//                     new PlayerInfoForMatch()
//                     {
//                         AccountId = 1,
//                         IsBot = false,
//                         WarshipName = "dich",
//                         ServiceId = 1.ToString(),
//                         TemporaryId = 25,
//                         WarshipPowerLevel = 5
//                     }
//                 }
//             };
//             //Act
//             bool success1 = gameUnits[0] == gameUnits.Players[0];
//             bool success2 = gameUnits[1] == gameUnits.Bots[0];
//             
//             //Assert
//             Assert.IsTrue(success1);
//             Assert.IsTrue(success2);
//         }
//         
//         /// <summary>
//         /// При неправильном индексе будет брошено исключение
//         /// </summary>
//         [ExpectedException(typeof(IndexOutOfRangeException))]
//         [TestMethod]
//         public void Test3()
//         {
//             //Arrange
//             GameUnits gameUnits = new GameUnits()
//             {
//                 Bots = new List<BotModel>
//                 {
//                     new BotModel()
//                     {
//                         IsBot = true,
//                         WarshipName = "dich",
//                         TemporaryId = 25,
//                         WarshipPowerLevel = 5
//                     }
//                 },
//                 Players = new List<PlayerInfoForMatch>
//                 {
//                     new PlayerInfoForMatch()
//                     {
//                         AccountId = 1,
//                         IsBot = false,
//                         WarshipName = "dich",
//                         ServiceId = 1.ToString(),
//                         TemporaryId = 25,
//                         WarshipPowerLevel = 5
//                     }
//                 }
//             };
//             //Act
//             var dich = gameUnits[2];
//             
//         }
//         
//         /// <summary>
//         /// Если ботов нет, то кол-во считается нормально
//         /// </summary>
//         [TestMethod]
//         public void Test4()
//         {
//             //Arrange
//             GameUnits gameUnits = new GameUnits()
//             {
//                 Players = new List<PlayerInfoForMatch>
//                 {
//                     new PlayerInfoForMatch()
//                     {
//                         AccountId = 1,
//                         IsBot = false,
//                         WarshipName = "dich",
//                         ServiceId = 1.ToString(),
//                         TemporaryId = 25,
//                         WarshipPowerLevel = 5
//                     }
//                 }
//             };
//             
//             //Act
//             int count = gameUnits.Count();
//             
//             //Assert
//             Assert.AreEqual(1,count);
//         }
//         
//         /// <summary>
//         /// Если игроков нет, то кол-во считается нормально
//         /// </summary>
//         [TestMethod]
//         public void Test5()
//         {
//             //Arrange
//             GameUnits gameUnits = new GameUnits()
//             {
//                 Bots = new List<BotModel>
//                 {
//                     new BotModel()
//                     {
//                         IsBot = true,
//                         WarshipName = "dich",
//                         TemporaryId = 25,
//                         WarshipPowerLevel = 5
//                     }
//                 }
//             };
//             //Act
//             int count = gameUnits.Count();
//             
//             //Assert
//             Assert.AreEqual(1,count);
//         }
//         
//         /// <summary>
//         /// Если игроков нет, то кол-во считается нормально
//         /// </summary>
//         [TestMethod]
//         public void Test6()
//         {
//             //Arrange
//             GameUnits gameUnits = new GameUnits()
//             {
//                 Bots = new List<BotModel>
//                 {
//                     new BotModel()
//                     {
//                         IsBot = true,
//                         WarshipName = "dich",
//                         TemporaryId = 25,
//                         WarshipPowerLevel = 5
//                     },
//                     new BotModel()
//                     {
//                         IsBot = true,
//                         WarshipName = "dich",
//                         TemporaryId = 25,
//                         WarshipPowerLevel = 5
//                     }
//                 }
//             };
//             //Act
//             int count = gameUnits.Count();
//             
//             //Assert
//             Assert.AreEqual(2,count);
//         }
//         
//         /// <summary>
//         /// Проверка foreach
//         /// </summary>
//         [TestMethod]
//         public void Test7()
//         {
//             //Arrange
//             GameUnits gameUnits = new GameUnits()
//             {
//                 Bots = new List<BotModel>
//                 {
//                     new BotModel()
//                     {
//                         IsBot = true,
//                         WarshipName = "dich",
//                         TemporaryId = 25,
//                         WarshipPowerLevel = 5
//                     },
//                     new BotModel()
//                     {
//                         IsBot = true,
//                         WarshipName = "dich",
//                         TemporaryId = 25,
//                         WarshipPowerLevel = 5
//                     }
//                 }
//             };
//             
//             //Act
//             int counter = 0;
//             foreach (GameUnit gameUnit in gameUnits)
//             {
//                 counter++;
//             }
//             
//             //Assert
//             Assert.AreEqual(2, counter);
//         }
//     }
// }