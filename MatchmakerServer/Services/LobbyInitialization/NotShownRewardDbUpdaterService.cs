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
    //TODO убрать ToListAsync
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
        public async Task<RewardsThatHaveNotBeenShown> GetNotShownResultsAndMarkAsRead([NotNull] string playerServiceId)
        {
            Account account = await dbContext.Accounts
                .SingleOrDefaultAsync(account1 => account1.ServiceId == playerServiceId);

            if (account == null)
            {
                return null;
            }

            RewardsThatHaveNotBeenShown result = new RewardsThatHaveNotBeenShown();
            result += await GetUnshownMatchReward(account.Id);
            result += await GetUnshownLootboxAward(account.Id);
            
            await dbContext.SaveChangesAsync();
            return result;
        }

        private async Task<RewardsThatHaveNotBeenShown> GetUnshownMatchReward(int accountId)
        {
            RewardsThatHaveNotBeenShown result = new RewardsThatHaveNotBeenShown(); 
            
            //Список законченных боёв, результат которых не был показан
            List<MatchResultForPlayer> matchResults =  await dbContext
                .MatchResultForPlayers
                .Where(player => player.Warship.AccountId == accountId 
                                 && !player.WasShown 
                                 && player.RegularCurrencyDelta != null)
                .ToListAsync();
            
            for (var index = 0; index < matchResults.Count; index++)
            {
                var matchResultForPlayer = matchResults[index];
                result.Rating += matchResultForPlayer.WarshipRatingDelta ?? 0;
                result.PremiumCurrency += matchResultForPlayer.PremiumCurrencyDelta ?? 0;
                result.RegularCurrency += matchResultForPlayer.RegularCurrencyDelta ?? 0;
                result.PointsForBigLootbox += matchResultForPlayer.PointsForBigChest ?? 0;
                result.PointsForSmallLootbox += matchResultForPlayer.PointsForSmallChest ?? 0;
                //Пометить как прочитанное
                matchResultForPlayer.WasShown = true;
            }

            return result;
        }

        private async Task<RewardsThatHaveNotBeenShown> GetUnshownLootboxAward(int accountId)
        {
            RewardsThatHaveNotBeenShown result = new RewardsThatHaveNotBeenShown();
            List<LootboxDb> lootboxes = await dbContext.Lootbox
                .Where(lootbox => lootbox.Account.Id == accountId
                                  && !lootbox.WasShown)
                
                .ToListAsync();
            
            for (int index = 0; index < lootboxes.Count; index++)
            {
                var lootboxDb = lootboxes[index];
                foreach (var regularCurrencyPrize in lootboxDb.LootboxPrizeRegularCurrencies)
                {
                    result.RegularCurrency += regularCurrencyPrize.Quantity;
                }
                foreach (var smallLootboxPrize in lootboxDb.LootboxPrizePointsForSmallChests)
                {
                    result.PointsForSmallLootbox += smallLootboxPrize.Quantity;
                }
                //Пометить как прочитанное
                lootboxDb.WasShown = true;
            }

            return result;
        }
    }
}