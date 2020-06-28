using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataLayer;
using DataLayer.Tables;
using Google.Apis.Upload;
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

        [ItemNotNull]
        public async Task<RewardsThatHaveNotBeenShown> GetNotShownRewardAndMarkAsRead([NotNull] string playerServiceId)
        {
            List<Transaction> transactions = await dbContext.Transactions
                .Include(transaction => transaction.Account)
                    .Include(transaction => transaction.Resources)
                        .ThenInclude(resource => resource.Increments)
                    .Include(transaction => transaction.Resources)
                        .ThenInclude(resource => resource.Decrements)
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
                accountRatingDelta = transaction.Resources
                    .SelectMany(resource => resource.Increments)
                    .Where(increment => increment.IncrementTypeId == IncrementTypeEnum.WarshipRating)
                    .Select(increment => increment.Amount)
                    .Sum();
                accountRatingDelta -= transaction.Resources
                    .SelectMany(resource => resource.Decrements)
                    .Where(decrement => decrement.DecrementTypeId == DecrementTypeEnum.WarshipRating)
                    .Select(decrement => decrement.Amount)
                    .Sum();
                
                //Премиум валюта
                hardCurrencyDelta = transaction.Resources
                    .SelectMany(resource => resource.Increments)
                    .Where(increment => increment.IncrementTypeId == IncrementTypeEnum.HardCurrency)
                    .Select(increment => increment.Amount)
                    .Sum();
                hardCurrencyDelta -= transaction.Resources
                    .SelectMany(resource => resource.Decrements)
                    .Where(decrement => decrement.DecrementTypeId == DecrementTypeEnum.HardCurrency)
                    .Select(decrement => decrement.Amount)
                    .Sum();
                
                //Обычная валюта
                softCurrencyDelta = transaction.Resources
                    .SelectMany(resource => resource.Increments)
                    .Where(increment => increment.IncrementTypeId == IncrementTypeEnum.SoftCurrency)
                    .Select(increment => increment.Amount)
                    .Sum();
                softCurrencyDelta -= transaction.Resources
                    .SelectMany(resource => resource.Decrements)
                    .Where(decrement => decrement.DecrementTypeId == DecrementTypeEnum.SoftCurrency)
                    .Select(decrement => decrement.Amount)
                    .Sum();
                
                //Очки для сундуков
                lootboxPointsDelta = transaction.Resources
                    .SelectMany(resource => resource.Increments)
                    .Where(increment => increment.IncrementTypeId == IncrementTypeEnum.LootboxPoints)
                    .Select(increment => increment.Amount)
                    .Sum();
                lootboxPointsDelta -= transaction.Resources
                    .SelectMany(resource => resource.Decrements)
                    .Where(decrement => decrement.DecrementTypeId == DecrementTypeEnum.LootboxPoints)
                    .Select(decrement => decrement.Amount)
                    .Sum();

                transaction.WasShown = true;
            }

            await dbContext.SaveChangesAsync();

            Console.WriteLine(accountRatingDelta);
            Console.WriteLine(hardCurrencyDelta);
            Console.WriteLine(lootboxPointsDelta);
            Console.WriteLine(softCurrencyDelta);

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