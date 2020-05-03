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
        
        public async Task<List<UiItemModel>> GetOrCreate([NotNull] string playerServiceId, int preferredCount)
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
            
            List<UiItemModel> result = new List<UiItemModel>(count);
            for (int index = 0; index < count; index++)
            {
                //TODO добавить улучшение для конкретного корабля
                UiItemModel uiItemModel = new UiItemModel()
                {

                };
            }

            return result;
        }
    }
}