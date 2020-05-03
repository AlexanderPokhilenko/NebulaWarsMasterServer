using System;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Случайно создёт приз для маленьгоко лутбокса.
    /// </summary>
    public class SmallLootboxPrizeFactory
    {
        private readonly Random random = new Random();
        
        public LootboxPrizeData Create()
        {
            LootboxPrizeType prizeType = (LootboxPrizeType) random.Next(2);
            int quantity;
            switch (prizeType)
            {
                case LootboxPrizeType.RegularCurrency:
                    quantity = random.Next(66);
                    break;
                case LootboxPrizeType.PointsForSmallLootbox:
                    quantity = random.Next(26);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            return new LootboxPrizeData
            {
                LootboxPrizeType = prizeType,
                Quantity = quantity
            };
        }
    }
}