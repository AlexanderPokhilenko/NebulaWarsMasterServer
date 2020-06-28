using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Проверяет, что у игрока достаточно баллов для открытия маленьгоко лутбокса.
    /// </summary>
    public class SmallLootboxOpenAllowingService
    {
        private readonly ApplicationDbContext dbContext;

        public SmallLootboxOpenAllowingService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CanPlayerOpenLootboxAsync(string playerServiceId)
        {
            int lootboxPoints = await dbContext.Increments
                .Include(increment => increment.Resource)
                    .ThenInclude(resource => resource.Transaction)
                        .ThenInclude(transaction => transaction.Account)
                .Where(increment => increment.IncrementTypeId == IncrementTypeEnum.LootboxPoints
                                    && increment.Resource.Transaction.Account.ServiceId == playerServiceId)
                .SumAsync(increment => increment.Amount);
            
            lootboxPoints -= await dbContext.Decrements
                .Include(decrement =>  decrement.Resource)
                    .ThenInclude(resource => resource.Transaction)
                        .ThenInclude(transaction => transaction.Account)
                .Where(decrement => decrement.DecrementTypeId == DecrementTypeEnum.LootboxPoints
                                    && decrement.Resource.Transaction.Account.ServiceId == playerServiceId)
                .SumAsync(decrement => decrement.Amount);
            
            if (lootboxPoints < 100)
            {
                return false;
            }

            return true;
        }
    }
}