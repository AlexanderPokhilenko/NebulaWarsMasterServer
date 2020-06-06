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

        public async Task<bool> CanPlayerOpenLootbox(string playerServiceId)
        {
            Account account = await dbContext.Accounts
                .Where(account1 => account1.ServiceId == playerServiceId)
                .SingleOrDefaultAsync();

            if (account == null)
            {
                return false;
            }

            if (account.SmallLootboxPoints < 100)
            {
                return false;
            }

            return true;
        }
    }
}