using AmoebaGameMatcherServer.Services.LobbyInitialization;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace IntegrationTests
{
    /// <summary>
    /// Отвечает за настройку БД и создание сервисов.
    /// </summary>
    [SetUpFixture]
    internal sealed class SetUpFixture
    {
        internal static ApplicationDbContext DbContext;
        internal static AccountDbReaderService Service;

        [OneTimeSetUp]
        public void Initialize()
        {
            string databaseName = "IntegrationTests";
            DbContext = new DbContextFactory().Create(databaseName);
            DbContext.Accounts.FromSql(new RawSqlString("ALTER DATABASE {0} SET postgres WITH ROLLBACK IMMEDIATE"), databaseName);
            
            // DbContext.Database.ExecuteSqlCommand(new RawSqlString("ALTER DATABASE {0} SET postgres WITH ROLLBACK IMMEDIATE"), databaseName);
            DbContext.Database.ExecuteSqlCommand(new RawSqlString("TRUNCATE TABLE \"Accounts\" CASCADE;"));

            //TODO seed here
            Service = new AccountDbReaderService(DbContext);
        }
    }
}



