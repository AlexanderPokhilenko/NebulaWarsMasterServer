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
        
        public LootboxPrizeModel Create(int[] warshipIds)
        {
            int length = Enum.GetNames(typeof(LootboxPrizeType)).Length;
            LootboxPrizeType prizeType = (LootboxPrizeType) random.Next(length);
            switch (prizeType)
            {
                case LootboxPrizeType.RegularCurrency:
                    return new LootboxPrizeModel
                    {
                        Quantity = random.Next(66),
                        LootboxPrizeType = LootboxPrizeType.RegularCurrency
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
                case LootboxPrizeType.PointsForSmallLootbox:
                    return new LootboxPrizeModel
                    {
                        Quantity = random.Next(44),
                        LootboxPrizeType = LootboxPrizeType.PointsForSmallLootbox
                    };
                default:
                    throw new Exception("Неизвестный тип подарка");
            }
        }
    }
}