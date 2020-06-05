using AmoebaGameMatcherServer;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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
            string databaseName = "IntegrationTests17";
            DbContext = new DbContextFactory().Create(databaseName);
            var seeder = new DataSeeder();
            seeder.TrySeed(DbContext);
            DbContext.Accounts.FromSql(new RawSqlString("ALTER DATABASE {0} SET postgres WITH ROLLBACK IMMEDIATE"), databaseName);
            TruncateAccountsTable();

            string connectionString = DbConfigIgnore.GetConnectionString(databaseName);
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            Service = new AccountDbReaderService(conn);
        }

        public static void TruncateAccountsTable()
        {
            DbContext.Database.ExecuteSqlCommand(new RawSqlString("TRUNCATE TABLE \"Accounts\" CASCADE;"));
        }
    }
}



