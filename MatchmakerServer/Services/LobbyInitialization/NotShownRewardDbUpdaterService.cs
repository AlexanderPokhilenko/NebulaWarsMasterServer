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
    /// Достаёт из БД бои, которые уже закончились, но для которых игроки не посмотрел начисление наград в ангаре.
    /// После доставания помечает результаты боя как просмотренные.
    /// </summary>
    public class NotShownRewardDbUpdaterService
    {
        private readonly ApplicationDbContext dbContext;

        public NotShownRewardDbUpdaterService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        [ItemCanBeNull]
        public async Task<RewardsThatHaveNotBeenShown> GetNotShownResults([NotNull] string playerServiceId)
        {
            Account account = await dbContext.Accounts
                .SingleOrDefaultAsync(account1 => account1.ServiceId == playerServiceId);

            if (account == null)
            {
                return null;
            }

            //Список законченных боёв, результат которых не был показан
            var matchResults =  await dbContext
                .MatchResultForPlayers
                .Where(player => player.Warship.AccountId == account.Id 
                                 && !player.WasShown 
                                 && player.RegularCurrencyDelta != null)
                .ToListAsync();
            
            
            RewardsThatHaveNotBeenShown result = new RewardsThatHaveNotBeenShown();

            for (var index = 0; index < matchResults.Count; index++)
            {
                var matchResultForPlayer = matchResults[index];
                result.Rating += matchResultForPlayer.WarshipRatingDelta ?? 0;
                result.PremiumCurrency += matchResultForPlayer.PremiumCurrencyDelta ?? 0;
                result.RegularCurrency += matchResultForPlayer.RegularCurrencyDelta ?? 0;
                result.PointsForBigChest += matchResultForPlayer.PointsForBigChest ?? 0;
                result.PointsForSmallChest += matchResultForPlayer.PointsForSmallChest ?? 0;
                matchResultForPlayer.WasShown = true;
            }
            await dbContext.SaveChangesAsync();
            
            return result;
        }
    }
}