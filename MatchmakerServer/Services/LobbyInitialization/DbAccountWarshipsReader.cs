using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataLayer.Tables;
using JetBrains.Annotations;
using Npgsql;

namespace AmoebaGameMatcherServer.Services.LobbyInitialization
{
    /// <summary>
    /// Достёт из БД данные про корабли аккаунта.
    /// </summary>
    public class DbAccountWarshipsReader
    {
        private readonly NpgsqlConnection connection;

        private readonly string sqlSelectAccountWarshipsInfo = $@"
                  --Достаёт всю информацию про корабли аккаунта
select a.*, w.*, wt.*, wcr.*,
        (select coalesce(sum(mr.""WarshipRatingDelta""), 0)
            from ""MatchResultForPlayers"" mr
                where mr.""WarshipId"" = w.""Id"")              	as ""WarshipRating"",
            (select sum(wpp.""Quantity"")
            from ""LootboxPrizeWarshipPowerPoints"" wpp
                where wpp.""WarshipId"" = w.""Id"")					as ""WarshipPowerPoints""
            from ""Accounts"" a
                inner join ""Warships"" w on a.""Id"" = w.""AccountId""
            inner join ""WarshipTypes"" wt on w.""WarshipTypeId"" = wt.""Id""
            inner join ""WarshipCombatRole"" wcr on wt.""WarshipCombatRoleId"" = wcr.""Id""
            left join ""MatchResultForPlayers"" matchResult on w.""Id"" = matchResult.""WarshipId""
            left join ""Lootbox"" lootbox on lootbox.""AccountId"" = a.""Id""    
            left join ""LootboxPrizeWarshipPowerPoints"" prizeWarshipPowerPoints on prizeWarshipPowerPoints.""LootboxId""=lootbox.""Id""
            where a.""ServiceId"" = @serviceIdPar
            group by a.""Id"",w.""Id"", wt.""Id"", wcr.""Id""

            ";

        public DbAccountWarshipsReader(NpgsqlConnection connection)
        {
            this.connection = connection;
        }
        
        public async Task<AccountDbDto> GetAccountWithWarshipsAsync([NotNull] string serviceId)
        {
            var parameters = new {serviceIdPar = serviceId};
            Dictionary<int, AccountDbDto> lookup = new Dictionary<int, AccountDbDto>();
            IEnumerable<AccountDbDto> accounts = await connection
                .QueryAsync<AccountDbDto, WarshipDbDto,WarshipType, WarshipCombatRole, AccountQueryDapperHelper1, AccountDbDto>(sqlSelectAccountWarshipsInfo,
                    (accountArg, warshipArg, warshipTypeArg, warshipCombatRole, dapperHelper) =>
                    {
                        //Если такого аккаунта ещё не было
                        if (!lookup.TryGetValue(accountArg.Id, out AccountDbDto account))
                        {
                            //Положить аккаунт в словарь
                            lookup.Add(accountArg.Id, account = accountArg);
                        }

                        //Попытаться достать корабль c таким id из коллекции
                        var warship = account.Warships.Find(wArg => wArg.Id == warshipArg.Id);
                        //Этот корабль уже есть в коллекции?
                        if (warship == null)
                        {
                            //Заполнить тип корабля и положить в коллекцию
                            warship = warshipArg;
                            warship.WarshipType = warshipTypeArg;
                            warship.WarshipRating = dapperHelper.WarshipRating;
                            warship.PowerPoints = dapperHelper.WarshipPowerPoints;
                            warship.WarshipType.WarshipCombatRole = warshipCombatRole;
                            
                            account.Warships.Add(warship);
                            account.Rating += warship.WarshipRating;
                        }

                        Console.WriteLine(" " + accountArg);
                        Console.WriteLine("\t\t " + warshipArg);
                        Console.WriteLine("\t\t\t " + dapperHelper);
                        return account;
                    }, parameters, splitOn:"Id,WarshipRating");
            
            switch (lookup.Count)
            {
                case 0:
                    return null;
                case 1 :
                    return lookup.Single().Value;
                default:
                    throw new Exception($"По serviceId = {serviceId} найдено {lookup.Count} аккаунтов.");
            }
        }
    }
}