using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataLayer;
using DataLayer.Tables;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using NetworkLibrary.NetworkLibrary.Http;
using Npgsql;

namespace AmoebaGameMatcherServer.Services.LobbyInitialization
{
    public class Test
    {
        public int WarshipRating { get; set; }
    }
    /// <summary>
    /// Во время загрузки данных в лобби достаёт аккаунт из БД.
    /// Если такого аккаунта нет, то вернёт null.
    /// </summary>
    public class AccountDbReaderService
    {
        private readonly NpgsqlConnection connection;

        public AccountDbReaderService(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Отвечает за получение данных про аккаунт из БД.
        /// </summary>
        [ItemCanBeNull]
        public async Task<Account> GetAccount([NotNull] string serviceId)
        {
            var parameters = new {serviceIdPar = serviceId};
            string sql = $@"
            select a.*, w.*, (sum(mr.""WarshipRatingDelta"")) as ""WarshipRating"", wt.*
                from ""Accounts"" a
                inner join ""Warships"" w on a.""Id"" = w.""AccountId""
                inner join ""WarshipTypes"" wt on w.""WarshipTypeId"" = wt.""Id""
                inner join ""MatchResultForPlayers"" mr on w.""Id"" = mr.""WarshipId""
                where a.""ServiceId"" = @serviceIdPar
                group by a.""Id"",w.""Id"", wt.""Id""
            ";

            Dictionary<int, Account> lookup = new Dictionary<int, Account>();
            IEnumerable<Account> accounts = await connection
                .QueryAsync<Account, Warship,Test, WarshipType, Account>(sql,
                    (a, w, t,wt) =>
                    {
                        Console.WriteLine(" " + a);
                        Console.WriteLine("\t\t " + w);
                        Console.WriteLine("\t\t\t " + t.WarshipRating);
                        Console.WriteLine("\t\t\t\t\t " + wt);

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
                            warship.WarshipType = wt;
                            account.Warships.Add(warship);
                            account.Rating += warship.WarshipRating;
                        }

                        return account;
                    }, parameters, splitOn:"Id,WarshipRating");

            Console.WriteLine("count "+lookup.Count);
            switch (lookup.Count)
            {
                case 0:
                    return null;
                case 1:
                    return lookup.Values.Single();
                default:
                    throw new Exception($"По serviceId = {serviceId} найдено {lookup.Count} аккаунтов.");
            }
        }
    }
}