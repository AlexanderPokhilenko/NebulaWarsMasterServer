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
        internal static LobbyModelFacadeService LobbyModelFacadeService;
        internal static AccountFacadeService AccountFacadeService;
        internal static LobbyModelController LobbyModelController;

        [OneTimeSetUp]
        public void Initialize()
        {
            string databaseName = "IntegrationTests30";
            //Создать БД
            DbContext = new DbContextFactory().Create(databaseName);
            //Ввести базовые данные
            var seeder = new DataSeeder();
            seeder.Seed(DbContext);
            //Прервать текущие сессии
            DbContext.Accounts.FromSql(new RawSqlString("ALTER DATABASE {0} SET postgres WITH ROLLBACK IMMEDIATE"), databaseName);
            //Очиста аккаунта
            TruncateAccountsTable();
            string connectionString = DbConfig.GetConnectionString(databaseName);
            //Создать сервисы
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            AccountReaderService = new AccountDbReaderService(conn);
            NotShownRewardsReaderService = new NotShownRewardsReaderService(DbContext);
            var accountRegistrationService = new AccountRegistrationService(DbContext);
            AccountFacadeService = new AccountFacadeService(AccountReaderService, accountRegistrationService);
            LobbyModelFacadeService = new LobbyModelFacadeService(AccountFacadeService, NotShownRewardsReaderService);

            LobbyModelController = new LobbyModelController(LobbyModelFacadeService);
        }

        public static void TruncateAccountsTable()
        {
            DbContext.Database.ExecuteSqlCommand(new RawSqlString("TRUNCATE TABLE \"Accounts\" CASCADE;"));
        }
    }
}



