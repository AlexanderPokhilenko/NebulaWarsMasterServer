using System.Collections.Generic;
using System.Linq;
using DataLayer;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer
{
    public class OrderTypesSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            if (!dbContext.OrderTypes.Any())
            {
                var orderTypes = new List<OrderType>
                {
                    new OrderType
                    {
                        Name = OrderTypeEnum.Lootbox.ToString(), 
                        Id = OrderTypeEnum.Lootbox
                    },
                    new OrderType
                    {
                        Name = OrderTypeEnum.LootboxSet.ToString(),
                        Id = OrderTypeEnum.LootboxSet
                    },
                    new OrderType
                    {
                        Name = OrderTypeEnum.Warship.ToString(),
                        Id = OrderTypeEnum.Warship
                    },
                    new OrderType
                    {
                        Name = OrderTypeEnum.WarshipAndSkin.ToString(),
                        Id = OrderTypeEnum.WarshipAndSkin
                    },
                    new OrderType
                    {
                        Name = OrderTypeEnum.Skin.ToString(),
                        Id = OrderTypeEnum.Skin
                    },
                    new OrderType
                    {
                        Name = OrderTypeEnum.WarshipPowerPoints.ToString(), 
                        Id = OrderTypeEnum.WarshipPowerPoints
                    },
                    new OrderType
                    {
                        Name = OrderTypeEnum.Prize.ToString(),
                        Id = OrderTypeEnum.Prize
                    },
                    new OrderType
                    {
                        Name = OrderTypeEnum.GameRegistration.ToString(), 
                        Id = OrderTypeEnum.GameRegistration
                    },
                    new OrderType
                    {
                        Name = OrderTypeEnum.SoftCurrency.ToString(),
                        Id = OrderTypeEnum.SoftCurrency
                    },
                    new OrderType
                    {
                        Name = OrderTypeEnum.HardCurrency.ToString(), 
                        Id = OrderTypeEnum.HardCurrency
                    },
                    new OrderType
                    {
                        Name = OrderTypeEnum.WarshipLevel.ToString(),
                        Id = OrderTypeEnum.WarshipLevel
                    }
                };
                dbContext.OrderTypes.AddRange(orderTypes);
                dbContext.SaveChanges();
            }
        }
    }
}