using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;
using Npgsql;

namespace AmoebaGameMatcherServer.Controllers
{
    public class NotShownRewardsReaderService
    {
        private readonly NpgsqlConnection connection;

        public NotShownRewardsReaderService(NpgsqlConnection connection)
        {
            this.connection = connection;
        }
        
        public async Task<RewardsThatHaveNotBeenShown> GetNotShownRewards([NotNull] string serviceId)
        {
            var parameters = new {serviceIdPar = serviceId};
            string sql = $@"
                        select 
                                (sum(mr.""SoftCurrencyDelta"") + sum(prRc.""Quantity"")) as ""SoftCurrency"",
                                (sum(mr.""SmallLootboxPoints"") + sum(prSl.""Quantity"")) as ""SmallLootboxPoints"",
                                (sum(prWpp.""Quantity"")) as ""WarshipPowerPoints""
                               
                                from ""Accounts"" a
                                inner join ""Warships"" w on w.""AccountId"" = a.""Id"" 
                                inner join ""MatchResultForPlayers"" mr on mr.""WarshipId"" = w.""Id""

                                inner join ""Lootbox"" lootbox on lootbox.""AccountId"" = a.""Id"" 
                                inner join ""LootboxPrizeSmallLootboxPoints"" prSl on prSl.""LootboxId"" = lootbox.""Id""
                                inner join ""LootboxPrizeSoftCurrency"" prRc on prRc.""LootboxId""=lootbox.""Id""
                                inner join ""LootboxPrizeWarshipPowerPoints"" prWpp on prWpp.""LootboxId""=lootbox.""Id""

                                where a.""ServiceId"" = @serviceIdPar and  mr.""WasShown""=false and lootbox.""WasShown""=false;
                          ";

            IEnumerable<RewardsThatHaveNotBeenShown> rewardsThatHaveNotBeenShown = await connection
                .QueryAsync<RewardsThatHaveNotBeenShown>(sql, parameters);

            RewardsThatHaveNotBeenShown result = rewardsThatHaveNotBeenShown.First();
            return result;
        }
    }
}