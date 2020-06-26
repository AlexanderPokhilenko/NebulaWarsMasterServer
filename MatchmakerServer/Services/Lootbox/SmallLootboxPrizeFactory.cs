using System;
using System.Runtime.InteropServices;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Случайно создёт приз для маленьгоко лутбокса.
    /// </summary>
    public class SmallLootboxPrizeFactory
    {
        private readonly Random random = new Random();
        private readonly int numberOfPrizeTypes;

        public SmallLootboxPrizeFactory()
        {
            numberOfPrizeTypes = Enum.GetNames(typeof(LootboxPrizeType)).Length;
        }
        
        public LootboxPrizeModel Create(int[] warshipIds)
        {
            LootboxPrizeType prizeType = (LootboxPrizeType) random.Next(numberOfPrizeTypes);
            switch (prizeType)
            {
                case LootboxPrizeType.SoftCurrency:
                    return new LootboxPrizeModel
                    {
                        Quantity = random.Next(66),
                        LootboxPrizeType = LootboxPrizeType.SoftCurrency
                    };
                case LootboxPrizeType.WarshipPowerPoints:
                {
                    int warshipIndex = random.Next(warshipIds.Length);
                    int warshipId = warshipIds[warshipIndex];
                    return new LootboxPrizeModel
                    {
                        Quantity = random.Next(66),
                        LootboxPrizeType = LootboxPrizeType.WarshipPowerPoints,
                        WarshipId = warshipId
                    };
                }
                case LootboxPrizeType.LootboxPoints:
                    return new LootboxPrizeModel
                    {
                        Quantity = random.Next(44),
                        LootboxPrizeType = LootboxPrizeType.LootboxPoints
                    };
                default:
                    throw new Exception("Неизвестный тип подарка");
            }
        }
    }
}