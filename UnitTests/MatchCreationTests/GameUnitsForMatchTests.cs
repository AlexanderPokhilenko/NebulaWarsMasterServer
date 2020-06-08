using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkLibrary.NetworkLibrary.Http;

//TODO это бесполезное говнище

namespace MatchmakerTest
{
    [TestClass]
    public class GameUnitsForMatchTests
    {
        /// <summary>
        /// Индексатор работает как я ожидаю
        /// </summary>
        [TestMethod]
        public void Test1()
        {
            //Arrange
            GameUnitsForMatch gameUnitsForMatch = new GameUnitsForMatch();
            List<PlayerInfoForMatch> playerInfoForMatches = new List<PlayerInfoForMatch>();
            List<BotInfo> botInfos = new List<BotInfo>();
            int countOfPlayers = 45;
            int countOfBots = 12;

            for (int i = 0; i < countOfPlayers; i++)
            {
                PlayerInfoForMatch playerInfoForMatch = new PlayerInfoForMatch()
                {
                    AccountId = i,
                    IsBot = false,
                    PrefabName = "dich",
                    ServiceId = i.ToString(),
                    TemporaryId = 25,
                    WarshipPowerPoints = 5
                };
                playerInfoForMatches.Add(playerInfoForMatch);
            }

            for (int i = 0; i < countOfBots; i++)
            {
                BotInfo botInfo = new BotInfo()
                {
                    IsBot = true,
                    PrefabName = "dich",
                    TemporaryId = 25,
                    WarshipPowerPoints = 5
                };
                botInfos.Add(botInfo);
            }

            gameUnitsForMatch.Players = playerInfoForMatches;
            gameUnitsForMatch.Bots = botInfos;

            //Act
            for (int i = 0; i < countOfPlayers; i++)
            {
                if (playerInfoForMatches[i] != gameUnitsForMatch[i])
                {
                    Assert.Fail();
                }
            }

            for (int i = countOfPlayers; i < countOfPlayers + countOfBots; i++)
            {
                if (botInfos[i - countOfPlayers] != gameUnitsForMatch[i])
                {
                    Assert.Fail();
                }
            }

            //Assert
        }

        /// <summary>
        /// Индексатор работает как я ожидаю
        /// </summary>
        [TestMethod]
        public void Test2()
        {
            //Arrange
            GameUnitsForMatch gameUnitsForMatch = new GameUnitsForMatch()
            {
                Bots = new List<BotInfo>
                {
                    new BotInfo()
                    {
                        IsBot = true,
                        PrefabName = "dich",
                        TemporaryId = 25,
                        WarshipPowerPoints = 5
                    }
                },
                Players = new List<PlayerInfoForMatch>
                {
                    new PlayerInfoForMatch()
                    {
                        AccountId = 1,
                        IsBot = false,
                        PrefabName = "dich",
                        ServiceId = 1.ToString(),
                        TemporaryId = 25,
                        WarshipPowerPoints = 5
                    }
                }
            };
            //Act
            bool success1 = gameUnitsForMatch[0] == gameUnitsForMatch.Players[0];
            bool success2 = gameUnitsForMatch[1] == gameUnitsForMatch.Bots[0];
            
            //Assert
            Assert.IsTrue(success1);
            Assert.IsTrue(success2);
        }
        
        /// <summary>
        /// При неправильном индексе будет брошено исключение
        /// </summary>
        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestMethod]
        public void Test3()
        {
            //Arrange
            GameUnitsForMatch gameUnitsForMatch = new GameUnitsForMatch()
            {
                Bots = new List<BotInfo>
                {
                    new BotInfo()
                    {
                        IsBot = true,
                        PrefabName = "dich",
                        TemporaryId = 25,
                        WarshipPowerPoints = 5
                    }
                },
                Players = new List<PlayerInfoForMatch>
                {
                    new PlayerInfoForMatch()
                    {
                        AccountId = 1,
                        IsBot = false,
                        PrefabName = "dich",
                        ServiceId = 1.ToString(),
                        TemporaryId = 25,
                        WarshipPowerPoints = 5
                    }
                }
            };
            //Act
            var dich = gameUnitsForMatch[2];
            
        }
        
        /// <summary>
        /// Если ботов нет, то кол-во считается нормально
        /// </summary>
        [TestMethod]
        public void Test4()
        {
            //Arrange
            GameUnitsForMatch gameUnitsForMatch = new GameUnitsForMatch()
            {
                Players = new List<PlayerInfoForMatch>
                {
                    new PlayerInfoForMatch()
                    {
                        AccountId = 1,
                        IsBot = false,
                        PrefabName = "dich",
                        ServiceId = 1.ToString(),
                        TemporaryId = 25,
                        WarshipPowerPoints = 5
                    }
                }
            };
            
            //Act
            int count = gameUnitsForMatch.Count();
            
            //Assert
            Assert.AreEqual(1,count);
        }
        
        /// <summary>
        /// Если игроков нет, то кол-во считается нормально
        /// </summary>
        [TestMethod]
        public void Test5()
        {
            //Arrange
            GameUnitsForMatch gameUnitsForMatch = new GameUnitsForMatch()
            {
                Bots = new List<BotInfo>
                {
                    new BotInfo()
                    {
                        IsBot = true,
                        PrefabName = "dich",
                        TemporaryId = 25,
                        WarshipPowerPoints = 5
                    }
                }
            };
            //Act
            int count = gameUnitsForMatch.Count();
            
            //Assert
            Assert.AreEqual(1,count);
        }
        
        /// <summary>
        /// Если игроков нет, то кол-во считается нормально
        /// </summary>
        [TestMethod]
        public void Test6()
        {
            //Arrange
            GameUnitsForMatch gameUnitsForMatch = new GameUnitsForMatch()
            {
                Bots = new List<BotInfo>
                {
                    new BotInfo()
                    {
                        IsBot = true,
                        PrefabName = "dich",
                        TemporaryId = 25,
                        WarshipPowerPoints = 5
                    },
                    new BotInfo()
                    {
                        IsBot = true,
                        PrefabName = "dich",
                        TemporaryId = 25,
                        WarshipPowerPoints = 5
                    }
                }
            };
            //Act
            int count = gameUnitsForMatch.Count();
            
            //Assert
            Assert.AreEqual(2,count);
        }
        
        /// <summary>
        /// Проверка foreach
        /// </summary>
        [TestMethod]
        public void Test7()
        {
            //Arrange
            GameUnitsForMatch gameUnitsForMatch = new GameUnitsForMatch()
            {
                Bots = new List<BotInfo>
                {
                    new BotInfo()
                    {
                        IsBot = true,
                        PrefabName = "dich",
                        TemporaryId = 25,
                        WarshipPowerPoints = 5
                    },
                    new BotInfo()
                    {
                        IsBot = true,
                        PrefabName = "dich",
                        TemporaryId = 25,
                        WarshipPowerPoints = 5
                    }
                }
            };
            
            //Act
            int counter = 0;
            foreach (GameUnit gameUnit in gameUnitsForMatch)
            {
                counter++;
            }
            
            //Assert
            Assert.AreEqual(2, counter);
        }
    }
}