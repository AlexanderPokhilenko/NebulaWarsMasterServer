using System;
using System.Collections.Generic;
using MatchmakerTest.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkLibrary.NetworkLibrary.Http;
using ZeroFormatter;

namespace MatchmakerTest
{
    [TestClass]
    public class ZeroFormatterDich
    {
        [TestMethod]
        public void Test1()
        {
            //Arrange
            List<WarshipModel> warships = new List<WarshipModel>()
            {
                new WarshipModel()
                {
                    Id = 45,
                    Rating = 54,
                    PrefabName = "prefabName1",
                    PowerLevel = 99,
                    PowerPoints = 42
                },
                new WarshipModel()
                {
                    Id = 312,
                    Rating = 52314,
                    PrefabName = "prefabName2",
                    PowerLevel = 9459,
                    PowerPoints = 43452
                }
            };
            AccountModel accountInfo = new AccountModel()
            {
                Username = UniqueStringFactory.Create(),
                AccountRating = 77,
                PremiumCurrency = 321,
                RegularCurrency = 88,
                PointsForBigLootbox = 219,
                PointsForSmallLootbox = 987,
                Warships = warships
            };

            //Act
            byte[] data = ZeroFormatterSerializer.Serialize(accountInfo);
            
            //Assert
            AccountModel accountInfoRestored = ZeroFormatterSerializer.Deserialize<AccountModel>(data);
            Assert.AreEqual(accountInfo.Username, accountInfoRestored.Username);
            
            foreach (var warship in accountInfoRestored.Warships)
            {
                Console.WriteLine(warship.PrefabName);
                Console.WriteLine(warship.Rating);
                Console.WriteLine(warship.Id);
                Console.WriteLine(warship.PowerPoints);
                Console.WriteLine(warship.PowerLevel);
            }
        }
    }
}