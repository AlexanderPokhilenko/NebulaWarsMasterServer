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
    /// <summary>
    /// Во время загрузки данных в лобби достаёт аккаунт из БД, если он есть.
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
            select a.*, w.""Id"", ""AccountId"", ""WarshipTypeId"", (sum(mr.""WarshipRatingDelta"")) as WarshipRating, wt.*
                from ""Accounts"" a
                inner join ""Warships"" w on a.""Id"" = w.""AccountId""
                inner join ""WarshipTypes"" wt on w.""WarshipTypeId"" = wt.""Id""
                inner join ""MatchResultForPlayers"" mr on w.""Id"" = mr.""WarshipId""
                where a.""ServiceId"" = @serviceIdPar
                group by a.""Id"",w.""Id"", wt.""Id""
            ";

            Dictionary<int, Account> lookup = new Dictionary<int, Account>();
            IEnumerable<Account> accounts = await connection
                .QueryAsync<Account, Warship, WarshipType, MatchResultForPlayer, Account>(sql,
                    (a, w, wt, mr) =>
                    {
                        Console.WriteLine(" " + a);
                        Console.WriteLine("\t\t " + w);
                        Console.WriteLine("\t\t\t " + mr);
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
                        }

                        //Обновить кол-во рейтинга на корабле и аккаунте
                        //Добавить результат боя к списку корабля
                        warship.MatchResultForPlayers.Add(mr);
                        return account;
                    }, parameters);

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