using System.Collections.Generic;
using System.Linq;
using DataLayer;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer
{
    public class DecrementTypeSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            if (!dbContext.DecrementTypes.Any())
            {
                var decrementTypes = new List<DecrementType>
                {
                    new DecrementType
                    {
                        Name = DecrementTypeEnum.LootboxPoints.ToString(),
                        Id = DecrementTypeEnum.LootboxPoints
                    },
                    new DecrementType
                    {
                        Name = DecrementTypeEnum.RealCurrency.ToString(),
                        Id = DecrementTypeEnum.RealCurrency
                    },
                    new DecrementType
                    {
                        Name = DecrementTypeEnum.WarshipRating.ToString(),
                        Id = DecrementTypeEnum.WarshipRating
                    },
                    new DecrementType
                    {
                        Name = DecrementTypeEnum.SoftCurrency.ToString(),
                        Id = DecrementTypeEnum.SoftCurrency
                    },
                    new DecrementType
                    {
                        Name = DecrementTypeEnum.HardCurrency.ToString(),
                        Id = DecrementTypeEnum.HardCurrency
                    }
                };
                dbContext.DecrementTypes.AddRange(decrementTypes);
                dbContext.SaveChanges();
            }
        }
    }
}