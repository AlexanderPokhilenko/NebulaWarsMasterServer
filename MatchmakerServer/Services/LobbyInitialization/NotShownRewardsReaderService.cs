using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.LobbyInitialization
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

        [ItemNotNull]
        public async Task<RewardsThatHaveNotBeenShown> GetNotShownRewardAndMarkAsRead([NotNull] string playerServiceId)
        {
            List<Transaction> transactions = await dbContext.Transactions
                .Include(transaction => transaction.Account)
                .Include(resource => resource.Increments)
                .Include(resource => resource.Decrements)
                .Where(transaction => transaction.Account.ServiceId == playerServiceId 
                                      && transaction.WasShown == false)
                .ToListAsync();

            if (transactions.Count == 0)
            {
                return new RewardsThatHaveNotBeenShown();
            }

            int accountRatingDelta=0;
            int hardCurrencyDelta=0;
            int softCurrencyDelta=0;
            int lootboxPointsDelta = 0;
            foreach (var transaction in transactions)
            {
                //Рейтинг
                accountRatingDelta += transaction
                    .Increments
                    .Where(increment => increment.IncrementTypeId == IncrementTypeEnum.WarshipRating)
                    .Select(increment => increment.Amount)
                    .Sum();
                
                //Премиум валюта
                hardCurrencyDelta += transaction.Increments
                    .Where(increment => increment.IncrementTypeId == IncrementTypeEnum.HardCurrency)
                    .Select(increment => increment.Amount)
                    .Sum();
                
                
                //Обычная валюта
                softCurrencyDelta += transaction.Increments
                    .Where(increment => increment.IncrementTypeId == IncrementTypeEnum.SoftCurrency)
                    .Select(increment => increment.Amount)
                    .Sum();
                
                
                //Очки для сундуков
                lootboxPointsDelta += transaction.Increments
                    .Where(increment => increment.IncrementTypeId == IncrementTypeEnum.LootboxPoints)
                    .Select(increment => increment.Amount)
                    .Sum();
                

                transaction.WasShown = true;
            }

            await dbContext.SaveChangesAsync();
        
            RewardsThatHaveNotBeenShown result = new RewardsThatHaveNotBeenShown()
            {
                AccountRatingDelta = accountRatingDelta,
                HardCurrencyDelta = hardCurrencyDelta,
                LootboxPointsDelta = lootboxPointsDelta,
                SoftCurrencyDelta = softCurrencyDelta
            };
            return result;
        }
    }
}