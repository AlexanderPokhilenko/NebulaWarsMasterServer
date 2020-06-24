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
                        Name = DecrementTypeEnum.GameCurrency.ToString(),
                        Id = DecrementTypeEnum.GameCurrency
                    },
                    new DecrementType
                    {
                        Name = DecrementTypeEnum.RealCurrency.ToString(),
                        Id = DecrementTypeEnum.RealCurrency
                    }
                };
                dbContext.DecrementTypes.AddRange(decrementTypes);
                dbContext.SaveChanges();
            }
        }
    }
}