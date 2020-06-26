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
            Account account = await dbContext.Accounts
                .Include(account1 => account1.Transactions)
                    .ThenInclude(transaction => transaction.Resources)
                        .ThenInclude(resource => resource.Increments.Where(increment => increment.IncrementTypeId==IncrementTypeEnum.LootboxPoints))
                .Include(account1 => account1.Transactions)
                    .ThenInclude(transaction => transaction.Resources)
                        .ThenInclude(resource => resource.Decrements
                    .Where(decrement => decrement.DecrementTypeId==DecrementTypeEnum.LootboxPoints))
                .Where(account1 => account1.ServiceId == playerServiceId)
                .SingleOrDefaultAsync();

            if (account == null)
            {
                return false;
            }

            int lootboxPoints = account.Transactions
                .SelectMany(transaction => transaction.Resources)
                .SelectMany(resource => resource.Increments)
                .Sum(increment => increment.LootboxPoints)
                                -
                            account.Transactions
                .SelectMany(transaction => transaction.Resources)
                .SelectMany(resource => resource.Decrements)
                .Sum(decrement => decrement.LootboxPoints);
            
           
            if (lootboxPoints < 100)
            {
                return false;
            }

            return true;
        }
    }
}