using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

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
                        WarshipTypeId = WarshipTypeEnum.Hare,
                        CurrentSkinTypeId = SkinTypeEnum.Hare
                    },
                    new Warship
                    {
                        WarshipTypeId = WarshipTypeEnum.Bird,
                        CurrentSkinTypeId = SkinTypeEnum.Bird
                    },
                    new Warship
                    {
                        WarshipTypeId = WarshipTypeEnum.Smiley,
                        CurrentSkinTypeId = SkinTypeEnum.Smiley
                    }
                },
                Transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        DateTime = DateTime.UtcNow,
                        TransactionTypeId = TransactionTypeEnum.GameRegistration,
                        WasShown = false,
                        Increments = new List<Increment>
                        {
                            new Increment
                            {
                                IncrementTypeId = IncrementTypeEnum.SoftCurrency,
                                Amount = 500
                            },
                            new Increment
                            {
                                IncrementTypeId = IncrementTypeEnum.HardCurrency,
                                Amount = 300
                            },
                            new Increment
                            {
                                IncrementTypeId = IncrementTypeEnum.LootboxPoints,
                                Amount = 1505
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
                TransactionTypeId = TransactionTypeEnum.DailyPrize,
                DateTime = DateTime.UtcNow,
                WasShown = false,
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
                    },
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.WarshipLevel,
                        Amount = 1,
                        WarshipId = account.Warships[2].Id
                    }
                }
            };
            
            //Добавить кораблям скины
            Transaction skinTransaction = new Transaction
            {
                AccountId = account.Id,
                TransactionTypeId = TransactionTypeEnum.GameRegistration,
                DateTime = DateTime.UtcNow,
                WasShown = false,
                Increments = new List<Increment>
                {
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.Skin,
                        Amount = 1,
                        SkinTypeId = SkinTypeEnum.Hare,
                        WarshipId = account.Warships[0].Id
                    },
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.Skin,
                        Amount = 1,
                        SkinTypeId = SkinTypeEnum.Bird,
                        WarshipId = account.Warships[1].Id
                    },   
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.Skin,
                        Amount = 1,
                        SkinTypeId = SkinTypeEnum.Raven,
                        WarshipId = account.Warships[1].Id
                    },
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.Skin,
                        Amount = 1,
                        SkinTypeId = SkinTypeEnum.Smiley,
                        WarshipId = account.Warships[2].Id
                    }
                }
            };
            
            await dbContext.Transactions.AddAsync(transaction);
            await dbContext.Transactions.AddAsync(skinTransaction);
            await dbContext.SaveChangesAsync();
        }
    }
}