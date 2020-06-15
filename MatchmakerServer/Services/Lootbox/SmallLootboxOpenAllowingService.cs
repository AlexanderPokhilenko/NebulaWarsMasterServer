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
            //TODO добавить нормальное чтения аккаунта
            Account account = await dbContext.Accounts
                .Where(account1 => account1.ServiceId == playerServiceId)
                .SingleOrDefaultAsync();

            if (account == null)
            {
                return false;
            }

            // //TODO добавить нормальное чтения аккаунта
            // if (account.SmallLootboxPoints < 100)
            // {
            //     return false;
            // }

            return true;
        }
    }
}