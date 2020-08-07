using System.Linq;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;

namespace AmoebaGameMatcherServer.Services.Database.Seeding.Seaders
{
    public class WarshipImprovementTrigger
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            if (!dbContext.GameModeTypes.Any())
            {
                string uniqueIndex = $@"CREATE UNIQUE INDEX unique_warship_improvement ON ""{nameof(Increment)}s""
 (""{nameof(Increment.Amount)}"", ""{nameof(Increment.WarshipId)}"") WHERE (""{nameof(Increment.IncrementTypeId)}"" =
 {(int)IncrementTypeEnum.WarshipLevel});";

                dbContext.Database.ExecuteSqlCommand(uniqueIndex);
            }
        }
    }
}