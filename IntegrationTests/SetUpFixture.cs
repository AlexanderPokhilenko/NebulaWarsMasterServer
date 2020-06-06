using AmoebaGameMatcherServer;
using AmoebaGameMatcherServer.Controllers;
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
        internal static AccountDbReaderService AccountReaderService;
        internal static NotShownRewardsReaderService NotShownRewardsReaderService;

        [OneTimeSetUp]
        public void Initialize()
        {
            string databaseName = "IntegrationTests26";
            //Создать БД
            DbContext = new DbContextFactory().Create(databaseName);
            //Ввести базовые данные
            var seeder = new DataSeeder();
            seeder.TrySeed(DbContext);
            //Прервать текущие сессии
            DbContext.Accounts.FromSql(new RawSqlString("ALTER DATABASE {0} SET postgres WITH ROLLBACK IMMEDIATE"), databaseName);
            //Очиста аккаунта
            TruncateAccountsTable();
            string connectionString = DbConfigIgnore.GetConnectionString(databaseName);
            //Создать сервисы
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            AccountReaderService = new AccountDbReaderService(conn);
            NotShownRewardsReaderService = new NotShownRewardsReaderService(conn);
        }

        public static void TruncateAccountsTable()
        {
            DbContext.Database.ExecuteSqlCommand(new RawSqlString("TRUNCATE TABLE \"Accounts\" CASCADE;"));
        }
    }
}



