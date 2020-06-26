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

            List<Increment> increments = new List<Increment>();
            foreach (LootboxPrizeModel prize in lootboxModel.Prizes)
            {
                Increment increment = new Increment();
                switch (prize.LootboxPrizeType)
                {
                    case LootboxPrizeType.SoftCurrency:
                        increment.IncrementTypeId = IncrementTypeEnum.SoftCurrency;
                        increment.SoftCurrency = prize.Quantity;
                        break;
                    case LootboxPrizeType.LootboxPoints:
                        increment.IncrementTypeId = IncrementTypeEnum.LootboxPoints;
                        increment.LootboxPoints = prize.Quantity;
                        break;
                    case LootboxPrizeType.WarshipPowerPoints:
                        if (prize.WarshipId != null)
                        {
                            increment.IncrementTypeId = IncrementTypeEnum.WarshipPowerPoints;
                            increment.WarshipPowerPoints = prize.Quantity;
                        }
                        else
                        {
                            throw new NullReferenceException(nameof(prize.WarshipId));
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                increments.Add(increment);
            }
            
            Resource resource = new Resource
            {
                Decrements = new List<Decrement>
                {
                    new Decrement
                    {
                        DecrementTypeId = DecrementTypeEnum.LootboxPoints,
                        LootboxPoints = 100
                    }
                },
                Increments = increments
            };

            Transaction transaction = new Transaction
            {
                AccountId = account.Id,
                DateTime = DateTime.UtcNow,
                TransactionTypeId = TransactionTypeEnum.Lootbox,
                Resources = new List<Resource>
                {
                    resource
                },
                WasShown = false
            };
            
            await dbContext.Transactions.AddAsync(transaction);
            await dbContext.SaveChangesAsync();
        }
    }
}