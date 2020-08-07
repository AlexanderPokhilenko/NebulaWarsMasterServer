using System;
using System.Collections.Generic;
using System.Linq;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Lootbox
{
    public class LootboxResourceTypeFactory
    {
        private readonly Random random;
        /// <summary>
        /// хранит значения в виде
        /// 0,2 - обычная валюта
        /// 0,6 - премиум валюта
        /// 1 - очки силы для корабля
        /// </summary>
        private readonly List<Tuple<double, ResourceTypeEnum>> list  = new List<Tuple<double, ResourceTypeEnum>>();
    
        public LootboxResourceTypeFactory(int? seed=null)
        {
            if (seed != null)
            {
                random = new Random(seed.Value);
            }
            else
            {
                random = new Random();
            }

            var tmpList = new List<Tuple<double, ResourceTypeEnum>>
            {
                new Tuple<double, ResourceTypeEnum>(29, ResourceTypeEnum.SoftCurrency),
                new Tuple<double, ResourceTypeEnum>(1, ResourceTypeEnum.HardCurrency),
                new Tuple<double, ResourceTypeEnum>(70, ResourceTypeEnum.WarshipPowerPoints)
            };

            double sum = tmpList.Sum(item => item.Item1);
            double tmpSum = 0;
            foreach (var tmpItem in tmpList)
            {
                double itemValue = (tmpItem.Item1 / sum) + tmpSum;
                list.Add(new Tuple<double, ResourceTypeEnum>(itemValue, tmpItem.Item2));
                tmpSum += tmpItem.Item1 / sum;
            }
        }
        public ResourceTypeEnum CreateResourceType()
        {
            double tmpValue = random.NextDouble();
            var test =  list.First(item => tmpValue <= item.Item1);

            return test.Item2;
        }
    }
}