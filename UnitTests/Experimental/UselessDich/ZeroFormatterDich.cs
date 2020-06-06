﻿using System;
using System.Collections.Generic;
using AmoebaGameMatcherServer.NetworkLibrary;
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
            List<WarshipDto> warships = new List<WarshipDto>()
            {
                new WarshipDto()
                {
                    Id = 45,
                    Rating = 54,
                    PrefabName = "prefabName1",
                    PowerLevel = 99,
                    PowerPoints = 42
                },
                new WarshipDto()
                {
                    Id = 312,
                    Rating = 52314,
                    PrefabName = "prefabName2",
                    PowerLevel = 9459,
                    PowerPoints = 43452
                }
            };
            AccountDto accountInfo = new AccountDto()
            {
                Username = UniqueStringFactory.Create(),
                AccountRating = 77,
                HardCurrency = 321,
                SoftCurrency = 88,
                BigLootboxPoints = 219,
                SmallLootboxPoints = 987,
                Warships = warships
            };

            //Act
            byte[] data = ZeroFormatterSerializer.Serialize(accountInfo);
            
            //Assert
            AccountDto accountInfoRestored = ZeroFormatterSerializer.Deserialize<AccountDto>(data);
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

        [TestMethod]
        public void TestKeyValuePair()
        {
            //Arrange
            TestKeyValuePair test = new TestKeyValuePair(new[]
            {
                new KeyValuePair<ushort, SevenBytes>(5,new SevenBytes(1,1,1,1,1,1,1)), 
            }, new[]
            {
                new KeyValuePair<ushort, ushort>(5,9) 
            });
            
            //Act
            
            byte[] data = ZeroFormatterSerializer.Serialize(test);
            TestKeyValuePair restoredPositionMessage = ZeroFormatterSerializer.Deserialize<TestKeyValuePair>(data);
            
            //Assert
            int expected = 4+4  +4  +2+7 ;
            Assert.AreEqual(expected, data.Length);
            CollectionAssert.AreEqual(test.__RadiusInfo, restoredPositionMessage.__RadiusInfo);
            CollectionAssert.AreEqual(test.EntitiesInfo, restoredPositionMessage.EntitiesInfo);
        }

        /// <summary>
        /// Тот же объект с Tuple больше на два байта
        /// </summary>
        [TestMethod]
        public void TuplePair()
        {
            //Arrange 
            TestTuple test = new TestTuple(new Tuple<ushort, SevenBytes>[]
            {
                new Tuple<ushort, SevenBytes>(4, new SevenBytes(1,1,1,1,1,1,1)) 
            }, new Tuple<ushort, ushort>[]
            {
                new Tuple<ushort, ushort>(1,5), 
            });
            
            //Act
            byte[] data = ZeroFormatterSerializer.Serialize(test);
            TestTuple restoredPositionMessage = ZeroFormatterSerializer.Deserialize<TestTuple>(data);
            
            //Assert
            int expected = 4+4  +4  +2+7 ;
            Assert.AreEqual(expected, data.Length);
            CollectionAssert.AreEqual(test.__RadiusInfo, restoredPositionMessage.__RadiusInfo);
            CollectionAssert.AreEqual(test.EntitiesInfo, restoredPositionMessage.EntitiesInfo);
        } 
        
        /// <summary>
        /// Тот же объект с Tuple больше на два байта
        /// </summary>
        [TestMethod]
        public void TestDictionary()
        {
            //Arrange 
            TestDictionaryStruct test = new TestDictionaryStruct(new Dictionary<ushort, SevenBytes>
            {
                {UInt16.MaxValue, new SevenBytes(1,1,1,11,1,1,1)}
            }, 
            new  Dictionary<ushort, ushort>
            {
                {1,5}
            });
            
            //Act
            byte[] data = ZeroFormatterSerializer.Serialize(test);
            TestDictionaryStruct restoredPositionMessage = ZeroFormatterSerializer.Deserialize<TestDictionaryStruct>(data);
            
            //Assert
            int expected = 4+4  +4  +2+7 ;
            Assert.AreEqual(expected, data.Length);
            CollectionAssert.AreEqual(test.__RadiusInfo, restoredPositionMessage.__RadiusInfo);
            CollectionAssert.AreEqual(test.EntitiesInfo, restoredPositionMessage.EntitiesInfo);
        }
    }

    public class UniqueStringFactory
    {
        public static string Create()
        {
            throw new NotImplementedException();
        }
    }
}