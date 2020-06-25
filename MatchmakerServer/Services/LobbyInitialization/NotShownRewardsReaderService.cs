using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataLayer;
using DataLayer.Tables;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using NetworkLibrary.NetworkLibrary.Http;
using Npgsql;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Читает из БД список наград, начисление которых не было показано и сразу помечает их как показанные.
    /// </summary>
    public class NotShownRewardsReaderService
    {
        private readonly ApplicationDbContext dbContext;

        public NotShownRewardsReaderService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        [ItemCanBeNull]
        public async Task<RewardsThatHaveNotBeenShown> GetNotShownResultsAndMarkAsReadAsync([NotNull] string playerServiceId)
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
            throw new Exception();
            // var result = new RewardsThatHaveNotBeenShown(); 
            //
            // //Список законченных боёв, результат которых не был показан
            // List<BattleRoyaleMatchResult> matchResults =  await dbContext
            //     .BattleRoyaleMatchResults
            //     .Where(matchResultForPlayer => matchResultForPlayer.Warship.AccountId == accountId 
            //                      && !matchResultForPlayer.WasShown 
            //                      && matchResultForPlayer.IsFinished)
            //     .ToListAsync();
            //
            // for (var index = 0; index < matchResults.Count; index++)
            // {
            //     BattleRoyaleMatchResult matchResultForPlayer = matchResults[index];
            //     result.AccountRating += matchResultForPlayer.WarshipRatingDelta;
            //     result.SoftCurrency += matchResultForPlayer.SoftCurrencyDelta;
            //     result.SmallLootboxPoints += matchResultForPlayer.SmallLootboxPoints;
            //     //Пометить как прочитанное
            //     matchResultForPlayer.WasShown = true;
            // }
            //
            // return result;
        }

        private async Task<RewardsThatHaveNotBeenShown> GetUnshownLootboxAward(int accountId)
        {
            // RewardsThatHaveNotBeenShown result = new RewardsThatHaveNotBeenShown();
            // List<LootboxDb> lootboxes = await dbContext.Lootbox
            //     .Include(lootbox => lootbox.LootboxPrizeSoftCurrency)
            //     .Include(lootbox => lootbox.LootboxPrizePointsForSmallLootboxes)
            //     .Where(lootbox => lootbox.Account.Id == accountId && !lootbox.WasShown)
            //     .ToListAsync();
            //
            // for (int index = 0; index < lootboxes.Count; index++)
            // {
            //     var lootboxDb = lootboxes[index];
            //     foreach (var softCurrencyPrize in lootboxDb.LootboxPrizeSoftCurrency)
            //     {
            //         result.SoftCurrency += softCurrencyPrize.Quantity;
            //     }
            //     foreach (var smallLootboxPointsPrize in lootboxDb.LootboxPrizePointsForSmallLootboxes)
            //     {
            //         result.LootboxPoints += smallLootboxPointsPrize.Quantity;
            //     }
            //     //Пометить как прочитанное
            //     lootboxDb.WasShown = true;
            // }

            return null;
        }
    }
}