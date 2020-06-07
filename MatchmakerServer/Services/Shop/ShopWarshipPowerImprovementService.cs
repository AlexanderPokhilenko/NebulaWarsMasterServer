using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Возвращает набор улучшений для кораблей игрока в зависимости от времени.
    /// </summary>
    public class ShopWarshipPowerImprovementService
    {
        private readonly ApplicationDbContext dbContext;

        public ShopWarshipPowerImprovementService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public async Task<List<ShopItemModel>> GetOrCreate([NotNull] string playerServiceId, int preferredCount)
        {
            Account account = await dbContext.Accounts
                .Where(account1 => account1.ServiceId == playerServiceId)
                .SingleOrDefaultAsync();

            if (account == null)
            {
                return null;
            }

            var warships = account.Warships;
            int count = Math.Min(preferredCount, warships.Count);
            
            List<ShopItemModel> result = new List<ShopItemModel>(count);
            for (int index = 0; index < count; index++)
            {
                //TODO добавить улучшение для конкретного корабля
                ShopItemModel uiItemModel = new ShopItemModel()
                {

                };
            }

            return result;
        }
    }
}