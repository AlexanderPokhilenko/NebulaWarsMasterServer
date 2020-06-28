using System;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;

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
            int currentWarshipRating = await dbContext.Transactions
                .Include(transaction => transaction.Resources)
                    .ThenInclude(resource => resource.Increments)
                .SelectMany(transaction => transaction.Resources)
                .SelectMany(resource => resource.Increments)
                .Where(increment => increment.WarshipId==warshipId)
                .SumAsync(increment => increment.Amount);
            
            currentWarshipRating -= await dbContext.Transactions
                .Include(transaction => transaction.Resources)
                .ThenInclude(resource => resource.Decrements)
                .SelectMany(transaction => transaction.Resources)
                .SelectMany(resource => resource.Decrements)
                .Where(decrement => decrement.WarshipId==warshipId)
                .SumAsync(decrement => decrement.Amount);
            
            return currentWarshipRating;
        }
    }
}