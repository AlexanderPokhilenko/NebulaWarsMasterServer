using System.Threading.Tasks;
using Dapper;
using JetBrains.Annotations;
using Npgsql;

namespace AmoebaGameMatcherServer.Services.LobbyInitialization
{
    /// <summary>
    /// Достаёт из БД данные про кол-во ресурсов у аккаунта
    /// </summary>
    public class AccountResourcesDbReader
    {
        private readonly string sqlSelectAccountResourcesInfo = $@"
--Достаёт информацию про ресурсы аккаунта
select 
       --Обычная валюта
(coalesce(
    (select sum(MRFP.""SoftCurrencyDelta"")
        from ""Accounts"" A1
            join ""Warships"" W on A1.""Id"" = W.""AccountId""
        join ""BattleRoyaleMatchResults"" MRFP on W.""Id"" = MRFP.""WarshipId""
        where A1.""ServiceId"" = @serviceIdPar
        )
        , 0) + 
        coalesce(
            (select sum(LPSC.""Quantity"") 
            from ""Accounts"" A
            join ""Lootbox"" L on A.""Id"" = L.""AccountId""
        join ""LootboxPrizeSoftCurrency"" LPSC on L.""Id"" = LPSC.""LootboxId""
        where A.""ServiceId"" = @serviceIdPar
        )
        , 0)) as ""SoftCurrency"",



        --Премиум валюта
        --Добавить таблицы с покупками
        (
            coalesce(
        (select sum(LPHC.""Quantity"")
            from ""Accounts"" A
            join ""Lootbox"" L on A.""Id"" = L.""AccountId""
        join ""LootboxPrizeHardCurrency"" LPHC on L.""Id"" = LPHC.""LootboxId""
        where A.""ServiceId"" = @serviceIdPar
        )
        , 0)) as ""HardCurrency"",



        --Баллы для маленького сундука
            (coalesce(
        (select sum(MRFP.""LootboxPoints"")
            from ""Accounts"" A1
            join ""Warships"" W on A1.""Id"" = W.""AccountId""
        join ""BattleRoyaleMatchResults"" MRFP on W.""Id"" = MRFP.""WarshipId""
        where A1.""ServiceId"" = @serviceIdPar
        )
        , 0) +
        coalesce(
            (select sum(LPSLP.""Quantity"")
            from ""Accounts"" A
            join ""Lootbox"" L on A.""Id"" = L.""AccountId""
        join ""LootboxPrizeSmallLootboxPoints"" LPSLP on L.""Id"" = LPSLP.""LootboxId""
        where A.""ServiceId"" = @serviceIdPar
        )
        , 0)) as ""LootboxPoints""
        ;";

        private readonly NpgsqlConnection connection;

        public AccountResourcesDbReader(NpgsqlConnection connection)
        {
            this.connection = connection;
        }
        
        public async Task<DapperHelperAccountResources> GetAccountResourcesAsync([NotNull] string serviceId)
        {
            var parameters = new {serviceIdPar = serviceId};
            var accountResources = await connection
                .QuerySingleAsync<DapperHelperAccountResources>(sqlSelectAccountResourcesInfo, parameters);

            return accountResources;
        }
    }
}