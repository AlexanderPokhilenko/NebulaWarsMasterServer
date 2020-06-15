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
        
        public async Task WriteAsync(string playerServiceId, LootboxModel lootboxModel)
        {
            Account account = await dbContext.Accounts
                .Where(account1 => account1.ServiceId == playerServiceId)
                .SingleOrDefaultAsync();

            //TODO создать сущность покупки лутбокса
            // account.SmallLootboxPoints -= 100;

            LootboxDb lootboxDb = new LootboxDb
            {
                LootboxType = LootboxType.Small,
                LootboxPrizePointsForSmallLootboxes = new List<LootboxPrizeSmallLootboxPoints>(),
                LootboxPrizeSoftCurrency = new List<LootboxPrizeSoftCurrency>(),
                LootboxPrizeWarshipPowerPoints = new List<LootboxPrizeWarshipPowerPoints>(),
                AccountId = account.Id,
                CreationDate = DateTime.UtcNow,
                WasShown = false
            };

            foreach (LootboxPrizeModel prize in lootboxModel.Prizes)
            {
                switch (prize.LootboxPrizeType)
                {
                    case LootboxPrizeType.SoftCurrency:
                        lootboxDb.LootboxPrizeSoftCurrency.Add(new LootboxPrizeSoftCurrency
                        {
                            Quantity = prize.Quantity
                        });
                        break;
                    case LootboxPrizeType.SmallLootboxPoints:
                        lootboxDb.LootboxPrizePointsForSmallLootboxes.Add(new LootboxPrizeSmallLootboxPoints
                        {
                            Quantity = prize.Quantity
                        });
                        break;
                    case LootboxPrizeType.WarshipPowerPoints:
                        if (prize.WarshipId != null)
                        {
                            var lootboxPrizeWarshipPowerPoints = new LootboxPrizeWarshipPowerPoints
                            {
                                Quantity = prize.Quantity,
                                WarshipId = prize.WarshipId.Value
                            };
                            lootboxDb.LootboxPrizeWarshipPowerPoints.Add(lootboxPrizeWarshipPowerPoints);
                        }
                        else
                        {
                            throw new Exception($"Не установлен {nameof(prize.WarshipId)}");
                        }
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