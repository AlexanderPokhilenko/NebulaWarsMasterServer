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
select        
(
    coalesce((select sum(I.""SoftCurrencyDelta"") 
        from ""Accounts"" A
            join ""Transactions"" O on A.""Id"" = O.""AccountId""
        join ""Resources"" P on O.""Id"" = P.""TransactionId""
        join ""Increments""  I on P.""Id"" = I.""ResourceId""
        where A.""ServiceId"" = @serviceIdPar), 0)
    
        -
        coalesce((select sum(D.""SoftCurrencyDelta"")
            from ""Accounts"" A
            join ""Transactions"" O on A.""Id"" = O.""AccountId""
        join ""Resources"" P on O.""Id"" = P.""TransactionId""
        join ""Decrements""  D on P.""Id"" = D.""ResourceId""
        where A.""ServiceId"" = @serviceIdPar), 0)

    
        ) as ""SoftCurrencyDelta"",

        (coalesce((select sum(I.""HardCurrencyDelta"")
            from ""Accounts"" A
            join ""Transactions"" T on A.""Id"" = T.""AccountId""
        join ""Resources"" R on T.""Id"" = R.""TransactionId""
        join ""Increments""  I on R.""Id"" = I.""ResourceId""
        where A.""ServiceId"" = @serviceIdPar),0)
        -
        coalesce((select sum(D.""HardCurrencyDelta"")
            from ""Accounts"" A
            join ""Transactions"" T on A.""Id"" = T.""AccountId""
        join ""Resources"" R on T.""Id"" = R.""TransactionId""
        join ""Decrements""  D on R.""Id"" = D.""ResourceId""
        where A.""ServiceId"" = @serviceIdPar),0)) as ""HardCurrencyDelta"",
    

        (coalesce(
        (select sum(I.""LootboxPoints"")
            from ""Accounts"" A
            join ""Transactions"" T on A.""Id"" = T.""AccountId""
        join ""Resources"" R on T.""Id"" = R.""TransactionId""
        join ""Increments""  I on R.""Id"" = I.""ResourceId""
        where A.""ServiceId"" = @serviceIdPar
        )
        , 0) 
        -
        coalesce(
            (select sum(D.""LootboxPoints"")
            from ""Accounts"" A
            join ""Transactions"" T on A.""Id"" = T.""AccountId""
        join ""Resources"" R on T.""Id"" = R.""TransactionId""
        join ""Decrements"" D on R.""Id"" = D.""ResourceId""
        where A.""ServiceId"" = @serviceIdPar
        )
        , 0))
        as ""LootboxPoints""
        ;

        ";

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