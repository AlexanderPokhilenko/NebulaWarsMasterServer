using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public static class DbContextFactory
    {
        public static ApplicationDbContext CreateDbContext()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = DbConfigIgnore.GetConnectionString();
            builder.UseNpgsql(connectionString);
            return new ApplicationDbContext(builder.Options);
        }
    }
}