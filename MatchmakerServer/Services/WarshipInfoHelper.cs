using System.Threading.Tasks;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
    public class WarshipInfoHelper
    {
     
        public async Task<WarshipInfo> GetWarshipInfo(int warshipId)
        {
            var dbContext = DbContextFactory.CreateDbContext();
            
            var warship = await dbContext.Warships
                .Include(warship1 => warship1.WarshipType)
                .SingleOrDefaultAsync(warship1 => warship1.Id==warshipId);

            WarshipInfo result = new WarshipInfo
            {
                Id = warship.Id,
                Rating = warship.WarshipRating,
                PrefabName = warship.WarshipType.Name,
                CombatPowerLevel = warship.WarshipCombatPowerLevel,
                CombatPowerValue = warship.WarshipCombatPowerValue
            };

            return result;
        }
    }
}