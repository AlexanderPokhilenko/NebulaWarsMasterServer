using Dapper;
using DataLayer;
using Npgsql;

namespace DeleteMe1
{
    public class AbsoluteDich
    {
        public long WarshipRating { get; set; }
    }

    public class RewardsThatHaveNotBeenShown
    {
        public virtual int RegularCurrency {get;set;}
        public virtual int PointsForSmallLootbox {get;set;}
    }
    public class Program
    {
        static void Main()
        {
            string databaseName = "DapperTests15";    
            string connectionString = DbConfigIgnore.GetConnectionString(databaseName);
            ApplicationDbContext dbContext = new DbContextFactory().Create(databaseName);

            DbWork dbWork = new DbWork(dbContext);
            // dbWork.TryInsert();
            string serviceId = dbWork.GetSomeServiceId();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                    var parameters = new {serviceIdPar = serviceId};
             
                string sql2 = $@"
                        select 
                                (sum(mr.""RegularCurrencyDelta"") + sum(prRc.""Quantity"")) as ""RegularCurrency"",
                                (sum(mr.""PointsForSmallLootbox"") + sum(prSl.""Quantity"")) as ""PointsForSmallLootbox"",
                                (sum(prWpp.""Quantity"")) as ""WarshipPowerPoints""
                               
                                from ""Accounts"" a
                                inner join ""Warships"" w on w.""AccountId"" = a.""Id"" 
                                inner join ""MatchResultForPlayers"" mr on mr.""WarshipId"" = w.""Id""

                                inner join ""Lootbox"" lootbox on lootbox.""AccountId"" = a.""Id"" 
                                inner join ""LootboxPrizePointsForSmallLootboxes"" prSl on prSl.""LootboxId"" = lootbox.""Id""
                                inner join ""LootboxPrizeRegularCurrencies"" prRc on prRc.""LootboxId""=lootbox.""Id""
                                inner join ""LootboxPrizeWarshipPowerPoints"" prWpp on prWpp.""LootboxId""=lootbox.""Id""

                                where a.""ServiceId"" = 'serviceId_13:12:05' and  mr.""WasShown""=false and lootbox.""WasShown""=false;
                          ";
                
                var result = connection.Query<RewardsThatHaveNotBeenShown>(sql2);
                foreach (var rewardsThatHaveNotBeenShown in result)
                {
                    
                }
            }
        }
    }
}