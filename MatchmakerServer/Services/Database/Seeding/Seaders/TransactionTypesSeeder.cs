using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer.Services.Database.Seeding.Seaders
{
    public class TransactionTypesSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            if (!dbContext.TransactionTypes.Any())
            {
                var transactionTypes = new List<TransactionType>
                {
                    new TransactionType
                    {
                        Name = TransactionTypeEnum.LootboxOpening.ToString(), 
                        Id = TransactionTypeEnum.LootboxOpening
                    },
                    new TransactionType
                    {
                        Name = TransactionTypeEnum.DailyPrize.ToString(),
                        Id = TransactionTypeEnum.DailyPrize
                    },
                    new TransactionType
                    {
                        Name = TransactionTypeEnum.GameRegistration.ToString(), 
                        Id = TransactionTypeEnum.GameRegistration
                    },
                    new TransactionType
                    {
                        Name = TransactionTypeEnum.MatchReward.ToString(),
                        Id = TransactionTypeEnum.MatchReward
                    },
                    new TransactionType
                    {
                        Name = TransactionTypeEnum.ShopPurchase.ToString(),
                        Id = TransactionTypeEnum.ShopPurchase
                    },
                    new TransactionType
                    {
                        Name = TransactionTypeEnum.WarshipImprovement.ToString(),
                        Id = TransactionTypeEnum.WarshipImprovement
                    }
                };
                dbContext.TransactionTypes.AddRange(transactionTypes);
                dbContext.SaveChanges();
            }
            
            if (dbContext.TransactionTypes.Count() != Enum.GetNames(typeof(TransactionTypeEnum)).Length)
            {
                int dbCount = dbContext.TransactionTypes.Count();
                int enumCount = Enum.GetNames(typeof(TransactionTypeEnum)).Length;
                throw new ArgumentOutOfRangeException($"{nameof(dbCount)} {dbCount} {nameof(enumCount)} {enumCount}");
            }
        }
    }
}