using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer.Services
{
    public class DefaultAccountFactoryService
    {
        private readonly ApplicationDbContext dbContext;

        public DefaultAccountFactoryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateDefaultAccountAsync(string playerId)
        {
            //сохранить аккаунт с кораблями
            Account account = new Account
            {
                ServiceId = playerId,
                Username = playerId,
                RegistrationDateTime = DateTime.UtcNow,
                Warships = new List<Warship>
                {
                    new Warship
                    {
                        WarshipTypeId = WarshipTypeEnum.Hare
                    },
                    new Warship
                    {
                        WarshipTypeId = WarshipTypeEnum.Bird
                    },
                    new Warship
                    {
                        WarshipTypeId = WarshipTypeEnum.Smiley
                    }
                },
                Transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        DateTime = DateTime.UtcNow,
                        TransactionTypeId = TransactionTypeEnum.GameRegistration,
                        WasShown = false,
                        Resources = new List<Resource>
                        {
                            new Resource
                            {
                                ResourceTypeId = ResourceTypeEnum.Prize,
                                Increments = new List<Increment>
                                {
                                    new Increment
                                    {
                                        IncrementTypeId = IncrementTypeEnum.SoftCurrency,
                                        Amount = 100
                                    },
                                    new Increment
                                    {
                                        IncrementTypeId = IncrementTypeEnum.HardCurrency,
                                        Amount = 30
                                    },
                                    new Increment
                                    {
                                        IncrementTypeId = IncrementTypeEnum.LootboxPoints,
                                        Amount = 1505
                                    }
                                }
                            }
                        }
                    }
                }
            };

            await dbContext.Accounts.AddAsync(account);
            await dbContext.SaveChangesAsync();
            
            //Присвоить кораблям первый уровень
            Transaction transaction = new Transaction
            {
                AccountId = account.Id,
                TransactionTypeId = TransactionTypeEnum.Prize,
                DateTime = DateTime.UtcNow,
                WasShown = false,
                Resources = new List<Resource>
                {
                    new Resource
                    {
                        ResourceTypeId = ResourceTypeEnum.WarshipLevel,
                        Increments = new List<Increment>
                        {
                            new Increment
                            {
                                IncrementTypeId = IncrementTypeEnum.WarshipLevel,
                                Amount = 1,
                                WarshipId = account.Warships[0].Id
                            },
                            new Increment
                            {
                                IncrementTypeId = IncrementTypeEnum.WarshipLevel,
                                Amount = 1,
                                WarshipId = account.Warships[1].Id
                            }
                        }
                    }
                }
            };

            await dbContext.Transactions.AddAsync(transaction);
            await dbContext.SaveChangesAsync();
        }
    }
}