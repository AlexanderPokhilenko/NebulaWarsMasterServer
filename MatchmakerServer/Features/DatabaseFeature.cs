using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer
{
    public class DatabaseFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            string connectionString = DbConfigIgnore.GetConnectionString();
            serviceCollection
                .AddEntityFrameworkNpgsql()
                .AddDbContext<ApplicationDbContext>(
                    opt => opt.UseNpgsql(connectionString))
                .BuildServiceProvider();

            serviceCollection.AddTransient<IDbContextFactory, DbContextFactory>();
        }
    }
}