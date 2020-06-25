using System.Linq;
using System.Threading.Tasks;
using Dapper;
using JetBrains.Annotations;
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

        private const string Sql = @"select T.""Id"",
        (coalesce(sum(I.""SoftCurrencyDelta""),0)-coalesce(sum(D.""SoftCurrencyDelta""),0)) as ""SoftCurrencyDelta"",
        (coalesce(sum(I.""HardCurrencyDelta""),0)-coalesce(sum(D.""HardCurrencyDelta""),0)) as ""HardCurrencyDelta"",       
        (coalesce(sum(I.""LootboxPoints""),0)-coalesce(sum(D.""LootboxPoints""),0)) as ""LootboxPointsDelta"",       
        (coalesce(sum(I.""WarshipRating""),0)-coalesce(sum(D.""WarshipRating""),0)) as ""AccountRatingDelta""
        from ""Accounts"" A
            inner join ""Transactions"" T on T.""AccountId"" = A.""Id""
        inner join ""Resources"" R on T.""Id"" = R.""TransactionId""   
        inner join ""Increments"" I on R.""Id"" = I.""ResourceId""   
        inner join ""Decrements"" D on R.""Id"" = D.""ResourceId""
        where A.""ServiceId"" = @serviceIdPar and  T.""WasShown"" = false
        group by T.""Id"";";
        
        public NotShownRewardsReaderService(NpgsqlConnection npgsqlConnection)
        {
            this.npgsqlConnection = npgsqlConnection;
        }
        
        [ItemCanBeNull]
        public async Task<RewardsThatHaveNotBeenShown> GetNotShownResults([NotNull] string playerServiceId)
        {
            var parameters = new {serviceIdPar = playerServiceId};
            var collection = await npgsqlConnection.QueryAsync<DapperHelperNotShownReward>(Sql,parameters);

            var element = collection.SingleOrDefault();
            if (element != null)
            {
                return new RewardsThatHaveNotBeenShown
                {
                    AccountRatingDelta = element.AccountRatingDelta,
                    HardCurrencyDelta = element.HardCurrencyDelta,
                    SoftCurrencyDelta = element.SoftCurrencyDelta,
                    LootboxPointsDelta = element.LootboxPointsDelta
                };
            }
            
            return null;
        }
    }
}