using DataLayer;
using DataLayer.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer.Features
{
    public class DatabaseFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            string connectionString = DbConnectionConfig.GetConnectionString();
            serviceCollection
                .AddEntityFrameworkNpgsql()
                .AddDbContext<ApplicationDbContext>(
                    opt => opt.UseNpgsql(connectionString))
                .BuildServiceProvider();

            serviceCollection.AddTransient<IDbContextFactory, DbContextFactory>();
        }
    }
}