using System;
using System.Collections.Generic;
using System.Linq;
using AmoebaGameMatcherServer.Services.Lootbox;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkLibrary.NetworkLibrary.Http;

namespace MatchmakerTest.Lootbox
{
    [TestClass]
    public class LootboxResourceTypeFactoryTests
    {
        [TestMethod]
        public void Test1()
        {
            LootboxResourceTypeFactory lootboxResourceTypeFactory = new LootboxResourceTypeFactory(146);
            Dictionary<ResourceTypeEnum, int > dict = new Dictionary<ResourceTypeEnum, int>();


            for (int i = 0; i < 1_000_000; i++)
            {
                var resourceType = lootboxResourceTypeFactory.CreateResourceType();
                dict.TryAdd(resourceType, 0);
                dict[resourceType] = dict[resourceType] + 1;
            }

            int sum = dict.Values.Sum();
            foreach (var pair in dict)
            {
                Console.WriteLine(pair.Key+"  "+1f*pair.Value/sum);
            }
        }
    }
}