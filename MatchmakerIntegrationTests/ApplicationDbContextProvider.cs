#define ConstantDbName

using DataLayer;
using DataLayer.Configuration;
using DataLayer.DbContextFactories;
using System;
using AmoebaGameMatcherServer.Services.Database.Seeding;

namespace MatchmakerIntegrationTests
{
    public static class ApplicationDbContextProvider
    {
#if ConstantDbName
        private const string DatabaseName = "DevelopmentDb500";  
#else
        private static readonly string DatabaseName = "TestDb_" + DateTime.Now.Ticks;
#endif

        static ApplicationDbContextProvider()
        {
            var context = GetContext();
            var seeder = new DataSeeder();
            seeder.Seed(context);
            context.SaveChanges();
        }

        public static ApplicationDbContext GetContext()
        {
            var dbConnectionConfig = new DbConnectionConfig(DatabaseName);
            var dbContextFactory = new DbContextFactory(dbConnectionConfig);
            return dbContextFactory.Create(DatabaseName);
        }
    }
}
