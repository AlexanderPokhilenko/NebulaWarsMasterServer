using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DataLayer;
using DataLayer.Tables;
using Npgsql;

namespace DeleteMe1
{
    public class Test
    {
        public virtual int WarshipRating { get; set; }
    }
    public class Program
    {
        static async Task Main()
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
             
                string sql = $@"
                    select a.*, w.*, wt.*, (sum(mr.""WarshipRatingDelta"")) as ""WarshipRating""
                        from ""Accounts"" a
                        inner join ""Warships"" w on a.""Id"" = w.""AccountId""
                        inner join ""WarshipTypes"" wt on w.""WarshipTypeId"" = wt.""Id""
                        inner join ""MatchResultForPlayers"" mr on w.""Id"" = mr.""WarshipId""
                        where a.""ServiceId"" = @serviceIdPar
                        group by a.""Id"",w.""Id"", wt.""Id""
                    ";
                
                Dictionary<int, Account> lookup = new Dictionary<int, Account>();
                IEnumerable<Account> accounts = await connection
                    .QueryAsync<Account, Warship,WarshipType, Test, Account>(sql,
                        (a, w, warshipType,test) =>
                        {
                            Console.WriteLine(" " + a);
                            Console.WriteLine("\t\t " + w);
                            Console.WriteLine("\t\t\t " + warshipType);
                            Console.WriteLine("\t\t\t\t\t " + test.WarshipRating);

                            //Если такого аккаунта ещё не было
                            if (!lookup.TryGetValue(a.Id, out Account account))
                            {
                                //Положить аккаунт в словарь
                                lookup.Add(a.Id, account = a);
                            }

                            //Попытаться достать корабль c таким id из коллекции
                            Warship warship = account.Warships.Find(wArg => wArg.Id == w.Id);
                            //Этот корабль уже есть в коллекции?
                            if (warship == null)
                            {
                                //Заполнить тип корабля и положить в коллекцию
                                warship = w;
                                warship.WarshipType = warshipType;
                                account.Warships.Add(warship);
                                account.Rating += warship.WarshipRating;
                            }

                            return account;
                        }, parameters, splitOn:"Id, WarshipRating");

            }
        }
    }
}