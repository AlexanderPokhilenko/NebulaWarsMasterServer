using System.Collections.Generic;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Lootbox
{
    /// <summary>
    /// Случайно создаёт маленький лутбокс с NumberOfPrizes призами.
    /// </summary>
    public class SmallLootboxDataFactory
    {
        private const int NumberOfPrizes = 3;
        private readonly SmallLootboxPrizeFactory lootboxPrizeFactory;

        public SmallLootboxDataFactory()
        {
            lootboxPrizeFactory = new SmallLootboxPrizeFactory();
        }
        
        public LootboxModel Create(int[] warshipIds)
        {
            LootboxModel result = new LootboxModel
            {
                Prizes = new List<LootboxPrizeModel>(NumberOfPrizes)
            };
            for (int i = 0; i < NumberOfPrizes; i++)
            {
                LootboxPrizeModel prize = lootboxPrizeFactory.Create(warshipIds);
                result.Prizes.Add(prize);
            }

            return result;
        }
    }
}