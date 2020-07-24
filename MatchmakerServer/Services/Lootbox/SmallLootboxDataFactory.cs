using System.Collections.Generic;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using DataLayer.Tables;
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
        
        public LootboxModel Create(List<WarshipDbDto> warships)
        {
            LootboxModel result = new LootboxModel
            {
                Prizes = new List<ResourceModel>(NumberOfPrizes)
            };
            for (int i = 0; i < NumberOfPrizes; i++)
            {
                ResourceModel prize = lootboxPrizeFactory.Create(warships);
                if (prize != null)
                {
                    result.Prizes.Add(prize);
                }
            }

            return result;
        }
    }
}