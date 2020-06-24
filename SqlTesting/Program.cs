using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using Dapper;
using DataLayer;
using DataLayer.Tables;
using Npgsql;

namespace DeleteMe1
{
    // public class Test
    // {
    //     public int WarshipRating { get; set; }
    //     public int SoftCurrency { get; set; }
    //     public int WarshipPowerPoints { get; set; }
    //     public int PointsForSmallLootboxes { get; set; }
    // }
    public class Program
    {
        static async Task Main()
        {
            string databaseName = "DapperTests30";    
            string connectionString = DbConfig.GetConnectionString(databaseName);
            ApplicationDbContext dbContext = new DbContextFactory().Create(databaseName);

            DbSeed dbSeed = new DbSeed(dbContext);
            dbSeed.TryInsert();
            string serviceId = dbSeed.GetSomeServiceId();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                  var parameters = new {serviceIdPar = serviceId};
            string sql = $@"
                  select a.*, w.*, wt.*,
       (
		   select sum(mr.""WarshipRatingDelta"")
            from ""MatchResultForPlayers"" mr
                where mr.""WarshipId"" = w.""Id""
                )									 		as ""WarshipRating"",
            (
                sum(matchResult.""SoftCurrency"") 
            + sum(COALESCE(prizeRegularCurrency.""Quantity"",0))
                )											as ""SoftCurrency"",
            (
                select sum(wpp.""Quantity"")
            from ""LootboxPrizeWarshipPowerPoints"" wpp
                where wpp.""WarshipId"" = w.""Id""
                )											as ""WarshipPowerPoints"",
            (
                sum(COALESCE(prizeSmallLootboxPoints.""Quantity"",0))
            + sum(matchResult.""LootboxPoints"")
                )											as ""PointsForSmallLootboxes""
            from ""Accounts"" a
                inner join ""Warships"" w on a.""Id"" = w.""AccountId""
            inner join ""WarshipTypes"" wt on w.""WarshipTypeId"" = wt.""Id""
            left join ""MatchResultForPlayers"" matchResult on w.""Id"" = matchResult.""WarshipId""
            left join ""Lootbox"" lootbox on lootbox.""AccountId"" = a.""Id""
            left join ""LootboxPrizeSmallLootboxPoints"" prizeSmallLootboxPoints on prizeSmallLootboxPoints.""LootboxId"" = lootbox.""Id""
            left join ""LootboxPrizeSoftCurrency"" prizeRegularCurrency on prizeRegularCurrency.""LootboxId""=lootbox.""Id""
            left join ""LootboxPrizeWarshipPowerPoints"" prizeWarshipPowerPoints on prizeWarshipPowerPoints.""LootboxId""=lootbox.""Id""
            where a.""ServiceId"" = 'serviceId_18:15:49'
            group by a.""Id"",w.""Id"", wt.""Id""
                ;
                    ";

            Dictionary<int, Account> lookup = new Dictionary<int, Account>();
            IEnumerable<Account> accounts = await connection
                .QueryAsync<Account, Warship,WarshipType, AccountQueryDapperHelper1,  Account>(sql,
                    (accountArg, warshipArg, warshipTypeArg,dapperHelper1) =>
                    {
                        Console.WriteLine(dapperHelper1.ToString());
                        return null;
                    }, parameters, splitOn:"Id,WarshipRating");

            }
        }
    }
}