using System;
using System.Threading.Tasks;
using DataLayer;

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
            throw new Exception();
            // int currentWarshipRating = await dbContext.Warships
            //     .Include(warship => warship.BattleRoyaleMatchResults)
            //     .Where(warship => warship.Id == warshipId)
            //     .SelectMany(warship => warship.BattleRoyaleMatchResults)
            //     .SumAsync(matchResult => matchResult.WarshipRatingDelta);
            //
            // return currentWarshipRating;
        }
    }
}