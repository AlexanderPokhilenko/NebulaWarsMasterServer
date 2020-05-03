using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Снимает со счёта игрока стоимость лутбокса. Сохраняет награды.
    /// </summary>
    public class LootboxDbWriterService
    {
        private readonly ApplicationDbContext dbContext;

        public LootboxDbWriterService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public async Task Write(string playerServiceId, LootboxData lootboxData)
        {
            Account account = await dbContext.Accounts
                .Where(account1 => account1.ServiceId == playerServiceId)
                .SingleOrDefaultAsync();

            account.PointsForSmallLootbox -= 100;

            LootboxDb lootboxDb = new LootboxDb
            {
                LootboxType = LootboxType.Small,
                LootboxPrizePointsForSmallChests = new List<LootboxPrizePointsForSmallLootbox>(),
                LootboxPrizeRegularCurrencies = new List<LootboxPrizeRegularCurrency>(),
                AccountId = account.Id,
                CreationDate = DateTime.UtcNow,
                WasShown = false
            };

            foreach (var prize in lootboxData.Prizes)
            {
                switch (prize.LootboxPrizeType)
                {
                    case LootboxPrizeType.RegularCurrency:
                        lootboxDb.LootboxPrizeRegularCurrencies.Add(new LootboxPrizeRegularCurrency
                        {
                            Quantity = prize.Quantity
                        });
                        break;
                    case LootboxPrizeType.PointsForSmallLootbox:
                        lootboxDb.LootboxPrizePointsForSmallChests.Add(new LootboxPrizePointsForSmallLootbox()
                        {
                            Quantity = prize.Quantity
                        });
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            await dbContext.Lootbox.AddAsync(lootboxDb);
            await dbContext.SaveChangesAsync();
        }
    }
}