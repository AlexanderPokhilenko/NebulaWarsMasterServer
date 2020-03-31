using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    /// <summary>
    /// Создаёт подключение к БД по connectionString
    /// </summary>
    public class DbContextFactory:IDbContextFactory
    {
        public ApplicationDbContext Create()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = DbConfigIgnore.GetConnectionString();
            builder.UseNpgsql(connectionString);
            return new ApplicationDbContext(builder.Options);
        }
    }
}