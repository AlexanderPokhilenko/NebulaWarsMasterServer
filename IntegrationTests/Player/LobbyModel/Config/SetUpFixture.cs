using System;
using AmoebaGameMatcherServer.Controllers.ProfileServer.Lobby;
using AmoebaGameMatcherServer.Experimental;
using AmoebaGameMatcherServer.Services;
using AmoebaGameMatcherServer.Services.Database.Seeding;
using AmoebaGameMatcherServer.Services.Experimental;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using DataLayer;
using DataLayer.Configuration;
using DataLayer.DbContextFactories;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NUnit.Framework;

namespace IntegrationTests.Player.LobbyModel.Config
{
    /// <summary>
    /// Отвечает за настройку БД и создание сервисов.
    /// </summary>
    [SetUpFixture]
    internal sealed class SetUpFixture
    {
        internal static ApplicationDbContext DbContext;
        private const string DatabaseName = "DevelopmentDb500";
        internal static AccountFacadeService AccountFacadeService;
        internal static LobbyModelController LobbyModelController;
        internal static AccountDbReaderService AccountReaderService;
        internal static LobbyModelFacadeService LobbyModelFacadeService;
        internal static DefaultAccountFactoryService DefaultAccountFactoryService;
        internal static NotShownRewardsReaderService NotShownRewardsReaderService;
        internal static WarshipImprovementCostChecker WarshipImprovementCostChecker;
        internal static WarshipImprovementFacadeService WarshipImprovementFacadeService;
        private static DbContextFactory dbContextFactory;

        [OneTimeSetUp]
        public static void Initialize()
        {
            Console.WriteLine("Initialization");
            IDbConnectionConfig dbConnectionConfig = new DbConnectionConfig(DatabaseName); 
            //Создать БД
            dbContextFactory = new DbContextFactory(dbConnectionConfig);
            DbContext = dbContextFactory.Create(DatabaseName);
            //Ввести базовые данные
            var seeder = new DataSeeder();
            seeder.Seed(DbContext);
            //Прервать текущие сессии
            DbContext.Accounts.FromSql(new RawSqlString("ALTER DATABASE {0} SET postgres WITH ROLLBACK IMMEDIATE"), DatabaseName);
            //Очиста аккаунта
            TruncateAccountsTable();
            string connectionString = dbConnectionConfig.GetConnectionString();
            //Создать сервисы
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connectionString);
            ISkinsDbReaderService skinsDbReaderService = new ISkinsDbReaderService(DbContext);
            IDbWarshipsStatisticsReader dbWarshipsStatisticsReader = new IDbWarshipsStatisticsReader(npgsqlConnection);
            IDbAccountWarshipReaderService dbAccountWarshipReaderService = new IDbAccountWarshipReaderService(dbWarshipsStatisticsReader, skinsDbReaderService);
            AccountResourcesDbReader accountResourcesDbReader = new AccountResourcesDbReader(npgsqlConnection);
            AccountReaderService = new AccountDbReaderService(dbAccountWarshipReaderService, accountResourcesDbReader);
            NotShownRewardsReaderService = new NotShownRewardsReaderService(DbContext);
            DefaultAccountFactoryService = new DefaultAccountFactoryService(DbContext);
            var accountRegistrationService = new AccountRegistrationService(DefaultAccountFactoryService);
            var warshipsCharacteristicsService = new WarshipsCharacteristicsService();
            AccountMapperService accountMapperService = new AccountMapperService(warshipsCharacteristicsService);
            AccountFacadeService = new AccountFacadeService(AccountReaderService, accountRegistrationService);
            var bundleVersionService = new BundleVersionService();
            
            LobbyModelFacadeService = new LobbyModelFacadeService(AccountFacadeService, NotShownRewardsReaderService,
                accountMapperService, bundleVersionService);
            
            UsernameValidatorService usernameValidatorService = new UsernameValidatorService();
            UsernameChangingService usernameChangingService = new UsernameChangingService(usernameValidatorService, DbContext);
            LobbyModelController = new LobbyModelController(LobbyModelFacadeService, usernameChangingService);
            WarshipImprovementCostChecker = new WarshipImprovementCostChecker();
            WarshipImprovementFacadeService = new WarshipImprovementFacadeService(AccountReaderService, DbContext, WarshipImprovementCostChecker);
            
        }

        public static void SetUp()
        {
            ReloadDbContext();
            TruncateAccountsTable();
        }

        private static void ReloadDbContext()
        {
            DbContext = dbContextFactory.Create(DatabaseName);
        }

        private static void TruncateAccountsTable()
        {
            DbContext.Database.ExecuteSqlCommand(new RawSqlString("TRUNCATE TABLE \"Accounts\" CASCADE;"));
        }
    }
}



