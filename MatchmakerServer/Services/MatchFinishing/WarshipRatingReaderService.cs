using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;

namespace AmoebaGameMatcherServer.Services.MatchFinishing
{
    public class WarshipRatingReaderService
    {
        private readonly ApplicationDbContext dbContext;

        public WarshipRatingReaderService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<int> ReadWarshipRatingAsync(int warshipId)
        {
            int currentWarshipRating = await dbContext.Increments
                .Where(increment => increment.WarshipId == warshipId 
                                    && increment.IncrementTypeId == IncrementTypeEnum.WarshipRating)
                .SumAsync(increment => increment.Amount);
            
            currentWarshipRating -= await dbContext.Decrements
                .Where(decrement => decrement.WarshipId == warshipId
                                    && decrement.DecrementTypeId==DecrementTypeEnum.WarshipRating)
                .SumAsync(decrement => decrement.Amount);
            
            return currentWarshipRating;
        }
    }
}