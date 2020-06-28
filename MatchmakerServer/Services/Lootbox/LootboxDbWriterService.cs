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
                        increment.Amount = prize.Quantity;
                        break;
                    case LootboxPrizeType.LootboxPoints:
                        increment.IncrementTypeId = IncrementTypeEnum.LootboxPoints;
                        increment.Amount = prize.Quantity;
                        break;
                    case LootboxPrizeType.WarshipPowerPoints:
                        if (prize.WarshipId != null)
                        {
                            increment.IncrementTypeId = IncrementTypeEnum.WarshipPowerPoints;
                            increment.Amount = prize.Quantity;
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
                ResourceTypeId = ResourceTypeEnum.Lootbox,
                Decrements = new List<Decrement>
                {
                    new Decrement
                    {
                        Amount = 100,
                        DecrementTypeId = DecrementTypeEnum.LootboxPoints
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