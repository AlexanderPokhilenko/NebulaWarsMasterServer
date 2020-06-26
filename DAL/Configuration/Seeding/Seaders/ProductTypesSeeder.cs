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
            if (!dbContext.ResourceTypes.Any())
            {
                var productTypes = new List<ResourceType>
                {
                    new ResourceType
                    {
                        Name = ResourceTypeEnum.Lootbox.ToString(),
                        Id = ResourceTypeEnum.Lootbox
                    },
                    new ResourceType
                    {
                        Name = ResourceTypeEnum.Warship.ToString(),
                        Id = ResourceTypeEnum.Warship
                    },
                    new ResourceType
                    {
                        Name = ResourceTypeEnum.Skin.ToString(),
                        Id = ResourceTypeEnum.Skin
                    },
                    new ResourceType 
                    {
                        Name = ResourceTypeEnum.WarshipPowerPoints.ToString(),
                        Id = ResourceTypeEnum.WarshipPowerPoints
                        
                    },
                    new ResourceType 
                    {
                        Name = ResourceTypeEnum.Prize.ToString(),
                        Id = ResourceTypeEnum.Prize
                    },
                    new ResourceType 
                    {
                        Name = ResourceTypeEnum.GameRegistration.ToString(), 
                        Id =  ResourceTypeEnum.GameRegistration
                    },
                    new ResourceType 
                    {
                        Name = ResourceTypeEnum.Currency.ToString(), 
                        Id =  ResourceTypeEnum.Currency
                    },
                    new ResourceType 
                    {
                        Name = ResourceTypeEnum.WarshipLevel.ToString(), 
                        Id = ResourceTypeEnum.WarshipLevel
                    },
                    new ResourceType 
                    {
                        Name = ResourceTypeEnum.MatchReward.ToString(), 
                        Id = ResourceTypeEnum.MatchReward
                    }
                };
                dbContext.ResourceTypes.AddRange(productTypes);
                dbContext.SaveChanges();
            }
        }
    }
}