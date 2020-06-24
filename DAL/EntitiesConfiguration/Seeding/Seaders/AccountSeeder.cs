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
                
                account.Orders = new List<Order>
                {
                    new Order
                    {
                        OrderTypeId = OrderTypeEnum.Lootbox,
                        Products = new List<Product>
                        {
                            new Product
                            {
                                ProductTypeId = ProductTypeEnum.Lootbox,
                                Increments = new List<Increment>
                                {
                                    new Increment
                                    {
                                        LootboxPowerPoints = 100,
                                        IncrementTypeId = IncrementTypeEnum.Currency
                                    }
                                },
                                Decrements = new List<Decrement>
                                {
                                    new Decrement
                                    {
                                        HardCurrency = 80,
                                        DecrementTypeId = DecrementTypeEnum.GameCurrency
                                    }
                                }
                            }
                        }
                    },
                    new Order
                    {
                        OrderTypeId = OrderTypeEnum.WarshipPowerPoints,
                        Products = new List<Product>
                        {
                            new Product
                            {
                                ProductTypeId = ProductTypeEnum.WarshipPowerPoints,
                                Increments = new List<Increment>
                                {
                                    new Increment
                                    {
                                        IncrementTypeId = IncrementTypeEnum.WarshipPowerPoints,
                                        WarshipId = 1,
                                        WarshipPowerPoints = 15
                                    }
                                }
                            }
                        }
                    }
                };
                
                dbContext.SaveChanges();
            }
        }
    }
}