using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Entities.Transactions.Decrement;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using NetworkLibrary.NetworkLibrary.Http;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Services.Lootbox
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
            foreach (ResourceModel prize in lootboxModel.Prizes)
            {
                Increment increment = new Increment();
                switch (prize.ResourceTypeEnum)
                {
                    case ResourceTypeEnum.SoftCurrency:
                    {
                        
                        increment.IncrementTypeId = IncrementTypeEnum.SoftCurrency;
                        int amount = ZeroFormatterSerializer
                            .Deserialize<SoftCurrencyResourceModel>(prize.SerializedModel).Amount;
                        increment.Amount = amount; 
                        break;
                    }
                    case ResourceTypeEnum.HardCurrency:
                    {
                        increment.IncrementTypeId = IncrementTypeEnum.HardCurrency;
                        int amount = ZeroFormatterSerializer
                            .Deserialize<HardCurrencyResourceModel>(prize.SerializedModel).Amount;
                        increment.Amount = amount; 
                        break;
                    }
                    case ResourceTypeEnum.WarshipPowerPoints:
                    {
                        var model = ZeroFormatterSerializer
                            .Deserialize<WarshipPowerPointsResourceModel>(prize.SerializedModel);
                        if (model.WarshipId != null)
                        {
                            increment.IncrementTypeId = IncrementTypeEnum.WarshipPowerPoints;
                            increment.Amount = model.FinishValue-model.StartValue;
                            increment.WarshipId = model.WarshipId;
                        }
                        else
                        {
                            throw new NullReferenceException("warshipId");
                        }
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                increments.Add(increment);
            }
            
           
            Transaction transaction = new Transaction
            {
                AccountId = account.Id,
                DateTime = DateTime.UtcNow,
                TransactionTypeId = TransactionTypeEnum.LootboxOpening,
                Decrements = new List<Decrement>
                {
                    new Decrement
                    {
                        Amount = 100,
                        DecrementTypeId = DecrementTypeEnum.LootboxPoints
                    }
                },
                Increments = increments,
                WasShown = false
            };
            
            await dbContext.Transactions.AddAsync(transaction);
            await dbContext.SaveChangesAsync();
        }
    }
}
