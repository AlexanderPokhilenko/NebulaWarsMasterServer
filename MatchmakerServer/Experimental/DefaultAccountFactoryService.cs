using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Experimental
{
    public class DefaultAccountFactoryService
    {
        private readonly ApplicationDbContext dbContext;

        public DefaultAccountFactoryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Account> CreateDefaultAccountAsync(string playerServiceId)
        {
            //сохранить аккаунт с кораблями
            Account account = new Account
            {
                ServiceId = playerServiceId,
                Username = playerServiceId,
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
                    },
                    new Warship
                    {
                        WarshipTypeId = WarshipTypeEnum.Sage,
                        CurrentSkinTypeId = SkinTypeEnum.Sage
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
                                Amount = 1
                            },
                            new Increment
                            {
                                IncrementTypeId = IncrementTypeEnum.HardCurrency,
                                Amount = 1
                            },
                            new Increment
                            {
                                IncrementTypeId = IncrementTypeEnum.LootboxPoints,
                                Amount = 100500
                            }
                        }
                    }
                }
            };

            await dbContext.Accounts.AddAsync(account);
            await dbContext.SaveChangesAsync();
            
            //Присвоить кораблям первый уровень
            Transaction warshipsLevelTransaction = new Transaction
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
                    },
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.WarshipLevel,
                        Amount = 1,
                        WarshipId = account.Warships[3].Id
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
                    },
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.Skin,
                        Amount = 1,
                        SkinTypeId = SkinTypeEnum.Sage,
                        WarshipId = account.Warships[3].Id
                    }
                }
            };
            
            //Добавить кораблям очки силы
            Transaction wppTransaction = new Transaction()
            {
                TransactionTypeId = TransactionTypeEnum.GameRegistration,
                AccountId = account.Id,
                DateTime = DateTime.UtcNow,
                WasShown = false,
                Increments = new List<Increment>()
                {
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.WarshipPowerPoints,
                        WarshipId = account.Warships[0].Id,
                        Amount = 15
                    },
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.WarshipPowerPoints,
                        WarshipId = account.Warships[1].Id,
                        Amount = 15
                    },
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.WarshipPowerPoints,
                        WarshipId = account.Warships[2].Id,
                        Amount = 15
                    },
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.WarshipPowerPoints,
                        WarshipId = account.Warships[3].Id,
                        Amount = 15
                    }
                }
            };
            
            await dbContext.Transactions.AddAsync(warshipsLevelTransaction);
            await dbContext.Transactions.AddAsync(skinTransaction);
            await dbContext.Transactions.AddAsync(wppTransaction);
            await dbContext.SaveChangesAsync();

            return account;
        }
    }
}