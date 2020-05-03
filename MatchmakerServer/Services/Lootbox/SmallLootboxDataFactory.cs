using System.Collections.Generic;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
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
        
        public LootboxData Create()
        {
            LootboxData result = new LootboxData()
            {
                Prizes = new List<LootboxPrizeData>(NumberOfPrizes)
            };
            for (int i = 0; i < NumberOfPrizes; i++)
            {
                LootboxPrizeData prize = lootboxPrizeFactory.Create();
                result.Prizes.Add(prize);
            }

            return result;
        }
    }
}