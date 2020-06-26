using System.Collections.Generic;
using System.Linq;
using DataLayer;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer
{
    public class IncrementTypeSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            if (!dbContext.IncrementTypes.Any())
            {
                var incrementTypes = new List<IncrementType>
                {
                    new IncrementType
                    {
                        Name = IncrementTypeEnum.Warship.ToString(),
                        Id = IncrementTypeEnum.Warship
                    },
                    new IncrementType
                    {
                        Name = IncrementTypeEnum.Skin.ToString(), 
                        Id = IncrementTypeEnum.Skin
                    },
                    new IncrementType
                    {
                        Name = IncrementTypeEnum.Currency.ToString(),
                        Id = IncrementTypeEnum.Currency
                    },
                    new IncrementType
                    {
                        Name = IncrementTypeEnum.WarshipPowerPoints.ToString(),
                        Id = IncrementTypeEnum.WarshipPowerPoints
                    },
                    new IncrementType
                    {
                        Name = IncrementTypeEnum.WarshipLevel.ToString(),
                        Id = IncrementTypeEnum.WarshipLevel
                    },
                    new IncrementType
                    {
                        Name = IncrementTypeEnum.Lootbox.ToString(),
                        Id = IncrementTypeEnum.Lootbox
                    }
                    ,
                    new IncrementType
                    {
                        Name = IncrementTypeEnum.WarshipRating.ToString(),
                        Id = IncrementTypeEnum.WarshipRating
                    }
                };
                dbContext.IncrementTypes.AddRange(incrementTypes);
                dbContext.SaveChanges();
            }
        }
    }
}