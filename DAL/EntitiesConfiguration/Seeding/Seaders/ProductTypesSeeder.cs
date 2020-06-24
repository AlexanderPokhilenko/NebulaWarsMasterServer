using System.Collections.Generic;
using System.Linq;
using DataLayer;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer
{
    public class ProductTypesSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            if (!dbContext.ProductTypes.Any())
            {
                var productTypes = new List<ProductType>
                {
                    new ProductType
                    {
                        Name = ProductTypeEnum.Lootbox.ToString(),
                        Id = ProductTypeEnum.Lootbox
                    },
                    new ProductType
                    {
                        Name = ProductTypeEnum.Warship.ToString(),
                        Id = ProductTypeEnum.Warship
                    },
                    new ProductType
                    {
                        Name = ProductTypeEnum.Skin.ToString(),
                        Id = ProductTypeEnum.Skin
                    },
                    new ProductType 
                    {
                        Name = ProductTypeEnum.WarshipPowerPoints.ToString(),
                        Id = ProductTypeEnum.WarshipPowerPoints
                        
                    },
                    new ProductType 
                    {
                        Name = ProductTypeEnum.Prize.ToString(),
                        Id = ProductTypeEnum.Prize
                    },
                    new ProductType 
                    {
                        Name = ProductTypeEnum.GameRegistration.ToString(), 
                        Id =  ProductTypeEnum.GameRegistration
                    },
                    new ProductType 
                    {
                        Name = ProductTypeEnum.Currency.ToString(), 
                        Id =  ProductTypeEnum.Currency
                    },
                    new ProductType 
                    {
                        Name = ProductTypeEnum.WarshipLevel.ToString(), 
                        Id = ProductTypeEnum.WarshipLevel
                    }
                };
                dbContext.ProductTypes.AddRange(productTypes);
                dbContext.SaveChanges();
            }
        }
    }
}