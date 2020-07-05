using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer
{
    public class AccountSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            if (!dbContext.Accounts.Any())
            {
                var account = new Account
                {
                    Username = "username",
                    ServiceId = "serviceId",
                    RegistrationDateTime = DateTime.UtcNow,
                    
                    Warships = new List<Warship>()
                    {
                        new Warship()
                        {
                            WarshipTypeId = WarshipTypeEnum.Hare
                        }
                    }
                };
                dbContext.Accounts.Add(account);
                dbContext.SaveChanges();

                account.Transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        TransactionTypeId = TransactionTypeEnum.Lootbox,
                        Increments = new List<Increment>
                        {
                            new Increment
                            {
                                Amount = 100,
                                IncrementTypeId = IncrementTypeEnum.LootboxPoints
                            }
                        },
                        Decrements = new List<Decrement>
                        {
                            new Decrement
                            {
                                Amount = 80,
                                DecrementTypeId = DecrementTypeEnum.HardCurrency
                            }
                        }
                    },
                    new Transaction
                    {
                        TransactionTypeId = TransactionTypeEnum.WarshipPowerPoints,
                        Increments = new List<Increment>
                        {
                            new Increment
                            {
                                IncrementTypeId = IncrementTypeEnum.WarshipPowerPoints,
                                WarshipId = 1,
                                Amount = 15
                            }
                        }
                    }
                };
            };
                
            dbContext.SaveChanges();
            }
        }
    }