using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class DbContextFactory
    {
        public ApplicationDbContext CreateDbContext()
        {
            DbConfig dbConfig = new DbConfig();
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = dbConfig.GetConnectionString();
            builder.UseNpgsql(connectionString);
            return new ApplicationDbContext(builder.Options);
        }
    }
}