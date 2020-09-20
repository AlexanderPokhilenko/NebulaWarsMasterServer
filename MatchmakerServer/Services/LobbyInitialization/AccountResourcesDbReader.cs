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
    coalesce((select sum(I.""Amount"") 
      from ""Accounts"" A
        join ""Transactions"" T on A.""Id"" = T.""AccountId""
        join ""Increments""  I on T.""Id"" = I.""TransactionId""
        join ""IncrementTypes"" IT on I.""IncrementTypeId"" = IT.""Id""    
        where A.""ServiceId"" = @serviceIdPar and it.""Name"" = 'SoftCurrency'
        
        ), 0)
    
    -
 coalesce((select sum(D.""Amount"")
    from ""Accounts"" A
        join ""Transactions"" T on A.""Id"" = T.""AccountId""
        join ""Decrements""  D on T.""Id"" = D.""TransactionId""
        join ""DecrementTypes"" DT on D.""DecrementTypeId"" = DT.""Id""
           where A.""ServiceId"" = @serviceIdPar and DT.""Name"" = 'SoftCurrency'
    ), 0)

    
    ) as ""SoftCurrency"",

(coalesce((select sum(I.""Amount"")
    from ""Accounts"" A
        join ""Transactions"" T on A.""Id"" = T.""AccountId""
        join ""Increments""  I on T.""Id"" = I.""TransactionId""
        join ""IncrementTypes"" IT on I.""IncrementTypeId"" = IT.""Id""
           where A.""ServiceId"" = @serviceIdPar and it.""Name"" = 'HardCurrency'
    ),0)
    -
    coalesce((select sum(D.""Amount"")
    from ""Accounts"" A
         join ""Transactions"" T on A.""Id"" = T.""AccountId""
        join ""Decrements""  D on T.""Id"" = D.""TransactionId""
         join ""DecrementTypes"" DT on D.""DecrementTypeId"" = DT.""Id""
              where A.""ServiceId"" = @serviceIdPar and DT.""Name"" = 'HardCurrency'
    ),0)) as ""HardCurrency"",
    

 (coalesce(
         (select sum(I.""Amount"")
          from ""Accounts"" A
           join ""Transactions"" T on A.""Id"" = T.""AccountId""
           join ""Increments""  I on T.""Id"" = I.""TransactionId""
           join ""IncrementTypes"" IT on I.""IncrementTypeId"" = IT.""Id""
          where A.""ServiceId"" = @serviceIdPar and it.""Name"" = 'LootboxPoints'
         )
     , 0) 
     -
  coalesce(
          (select sum(D.""Amount"")
           from ""Accounts"" A
                join ""Transactions"" T on A.""Id"" = T.""AccountId""
                join ""Decrements"" D on T.""Id"" = D.""TransactionId""
                join ""DecrementTypes"" DT on D.""DecrementTypeId"" = DT.""Id""
           where A.""ServiceId"" = @serviceIdPar and DT.""Name"" = 'LootboxPoints'
        
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
        
        public async Task<AccountResources> ReadAsync([NotNull] string serviceId)
        {
            var parameters = new {serviceIdPar = serviceId};
            var accountResources = await connection
                .QuerySingleAsync<AccountResources>(sqlSelectAccountResourcesInfo, parameters);

            return accountResources;
        }
    }
}