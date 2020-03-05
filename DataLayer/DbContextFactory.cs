using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class DbContextFactory
    {
        public ApplicationDbContext CreateDbContext()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = DbConfigIgnore.GetConnectionString();
            builder.UseNpgsql(connectionString);
            return new ApplicationDbContext(builder.Options);
        }
    }
}