using DataLayer;
using DataLayer.Configuration;
using DataLayer.DbContextFactories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace AmoebaGameMatcherServer.Features
{
    public class DatabaseFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            string databaseName = "r51";
            IDbConnectionConfig dbConnectionConfig = new DbConnectionConfig(databaseName);
            string connectionString = dbConnectionConfig.GetConnectionString();

            serviceCollection.AddTransient(provider => dbConnectionConfig);
            serviceCollection
                .AddEntityFrameworkNpgsql()
                .AddDbContext<ApplicationDbContext>(
                    opt => opt.UseNpgsql(connectionString))
                .BuildServiceProvider();

            serviceCollection.AddTransient<IDbContextFactory, DbContextFactory>();
            serviceCollection.AddTransient(provider => new NpgsqlConnection(connectionString));
        }
    }
}