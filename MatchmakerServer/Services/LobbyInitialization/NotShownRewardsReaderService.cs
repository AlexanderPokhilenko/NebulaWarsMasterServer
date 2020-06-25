using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataLayer;
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
        private readonly NpgsqlConnection npgsqlConnection;
        private readonly ApplicationDbContext dbContext;

        private const string Sql = @"select T.""Id"",
        (coalesce(sum(I.""SoftCurrency""),0)-coalesce(sum(D.""SoftCurrency""),0)) as ""SoftCurrency"",
        (coalesce(sum(I.""HardCurrency""),0)-coalesce(sum(D.""HardCurrency""),0)) as ""HardCurrencyDelta"",       
        (coalesce(sum(I.""LootboxPoints""),0)-coalesce(sum(D.""LootboxPoints""),0)) as ""LootboxPointsDelta"",       
        (coalesce(sum(I.""WarshipRating""),0)-coalesce(sum(D.""WarshipRating""),0)) as ""AccountRatingDelta""
        from ""Accounts"" A
            inner join ""Transactions"" T on T.""AccountId"" = A.""Id""
        left join ""Resources"" R on T.""Id"" = R.""TransactionId""   
        left join ""Increments"" I on R.""Id"" = I.""ResourceId""   
        left join ""Decrements"" D on R.""Id"" = D.""ResourceId""
        where A.""ServiceId"" = @serviceIdPar and  T.""WasShown"" = false
        group by T.""Id"";";
        
        public NotShownRewardsReaderService(NpgsqlConnection npgsqlConnection, ApplicationDbContext dbContext)
        {
            this.npgsqlConnection = npgsqlConnection;
            this.dbContext = dbContext;
        }

        [ItemNotNull]
        public async Task<RewardsThatHaveNotBeenShown> GetNotShownRewardAndMarkAsRead([NotNull] string playerServiceId)
        {
            var result = new RewardsThatHaveNotBeenShown();
            var parameters = new {serviceIdPar = playerServiceId};
            var collection = await npgsqlConnection
                .QueryAsync<DapperHelperNotShownReward>(Sql,parameters);

            DapperHelperNotShownReward[] dapperHelperNotShownRewards = collection as DapperHelperNotShownReward[]
                                                                       ?? collection.ToArray();
            if (dapperHelperNotShownRewards.Length != 0)
            {
                List<int> shownTransactionIds = new List<int>();
                foreach (var notShownReward in dapperHelperNotShownRewards)
                {
                    shownTransactionIds.Add(notShownReward.Id);
                    result.AccountRatingDelta += notShownReward.AccountRatingDelta;
                    result.HardCurrencyDelta += notShownReward.HardCurrencyDelta;
                    result.SoftCurrencyDelta += notShownReward.SoftCurrencyDelta;
                    result.LootboxPointsDelta += notShownReward.LootboxPointsDelta;
                }

                //транзакции помечаются как просмотренные
                foreach (var transactionId in shownTransactionIds)
                {
                    var transaction = await dbContext.Transactions
                        .SingleAsync(transaction1 => transaction1.Id == transactionId);
                    transaction.WasShown = true;
                }

                await dbContext.SaveChangesAsync();
            }
            
            return result;
        }
    }
}