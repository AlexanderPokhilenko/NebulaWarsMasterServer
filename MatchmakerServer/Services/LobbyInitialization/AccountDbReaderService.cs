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
                   select a.*, w.*, wt.*,
       (select sum(mr.""WarshipRatingDelta"")  
            from ""MatchResultForPlayers"" mr
                where mr.""WarshipId"" = w.""Id"")                                                    as ""WarshipRating"",
            (sum(matchResult.""RegularCurrencyDelta"") + sum(prizeRegulatCurrency.""Quantity""))      as ""RegularCurrency"",
            (select sum(wpp.""Quantity"")
            from ""LootboxPrizeWarshipPowerPoints"" wpp
                where wpp.""WarshipId"" = w.""Id"")                                                   as ""WarshipPowerPoints"",
            (sum(prizeSmallLootboxPoints.""Quantity"")+ sum(matchResult.""PointsForSmallLootbox"")) as ""PointsForSmallLootboxes""
            from ""Accounts"" a
                inner join ""Warships"" w on a.""Id"" = w.""AccountId""
            inner join ""WarshipTypes"" wt on w.""WarshipTypeId"" = wt.""Id""
            inner join ""MatchResultForPlayers"" matchResult on w.""Id"" = matchResult.""WarshipId""
            left join ""Lootbox"" lootbox on lootbox.""AccountId"" = a.""Id""
            left join ""LootboxPrizePointsForSmallLootboxes"" prizeSmallLootboxPoints on prizeSmallLootboxPoints.""LootboxId"" = lootbox.""Id""
            left join ""LootboxPrizeRegularCurrencies"" prizeRegulatCurrency on prizeRegulatCurrency.""LootboxId""=lootbox.""Id""
            left join ""LootboxPrizeWarshipPowerPoints"" prizeWarshipPowerPoints on prizeWarshipPowerPoints.""LootboxId""=lootbox.""Id""
where a.""ServiceId"" = @serviceIdPar
            group by a.""Id"",w.""Id"", wt.""Id""
            
                    ";

            Dictionary<int, Account> lookup = new Dictionary<int, Account>();
            IEnumerable<Account> accounts = await connection
                .QueryAsync<Account, Warship,WarshipType, AccountQueryDapperHelper1, Account>(sql,
                    (accountArg, warshipArg, warshipTypeArg, dapperHelper) =>
                    {
                        if (dapperHelper == null)
                        {
                            throw new Exception("Проблема в запросе");
                        }
                        //Если такого аккаунта ещё не было
                        if (!lookup.TryGetValue(accountArg.Id, out Account account))
                        {
                            //Положить аккаунт в словарь
                            lookup.Add(accountArg.Id, account = accountArg);
                            // account.RegularCurrency = dapperHelper.RegularCurrency;
                            // account.PointsForSmallLootbox = dapperHelper.PointsForSmallLootboxes;
                        }

                        //Попытаться достать корабль c таким id из коллекции
                        Warship warship = account.Warships.Find(wArg => wArg.Id == warshipArg.Id);
                        //Этот корабль уже есть в коллекции?
                        if (warship == null)
                        {
                            //Заполнить тип корабля и положить в коллекцию
                            warship = warshipArg;
                            warship.WarshipType = warshipTypeArg;
                            warship.WarshipRating = dapperHelper.WarshipRating;
                            // warship.PowerPoints = dapperHelper.WarshipPowerPoints;
                            account.Warships.Add(warship);
                            account.Rating += warship.WarshipRating;
                        }

                        Console.WriteLine(" " + accountArg);
                        Console.WriteLine("\t\t " + warshipArg);
                        Console.WriteLine("\t\t\t WarshipRating " + dapperHelper.WarshipRating);
                        // Console.WriteLine("\t\t\t RegularCurrency " + dapperHelper.RegularCurrency);
                        // Console.WriteLine("\t\t\t WarshipPowerPoints " + dapperHelper.WarshipPowerPoints);
                        // Console.WriteLine("\t\t\t PointsForSmallLootboxes " + dapperHelper.PointsForSmallLootboxes);
                        Console.WriteLine("\t\t\t\t\t " + warshipTypeArg);
                        
                        return account;
                    }, parameters, splitOn:"Id,WarshipRating,RegularCurrency,WarshipPowerPoints,PointsForSmallLootboxes");

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