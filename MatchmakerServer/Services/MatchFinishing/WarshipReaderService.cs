using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.EntityFrameworkCore;

namespace AmoebaGameMatcherServer.Services.MatchFinishing
{
    public class WarshipReaderService
    {
        private readonly ApplicationDbContext dbContext;

        public WarshipReaderService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<int> ReadWarshipRatingAsync(int warshipId)
        {
            int currentWarshipRating = await dbContext.Warships
                .Include(warship => warship.MatchResultForPlayers)
                .Where(warship => warship.Id == warshipId)
                .SelectMany(warship => warship.MatchResultForPlayers)
                .SumAsync(matchResult => matchResult.WarshipRatingDelta);

            return currentWarshipRating;
        }
    }
}