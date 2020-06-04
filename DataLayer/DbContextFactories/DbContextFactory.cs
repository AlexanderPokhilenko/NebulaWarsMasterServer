using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    /// <summary>
    /// Создаёт подключение к БД по connectionString
    /// </summary>
    public class DbContextFactory:IDbContextFactory
    {
        public ApplicationDbContext Create(string databaseName = null)
        {
            DbContextOptionsBuilder<ApplicationDbContext> builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            
            string connectionString = DbConfigIgnore.GetConnectionString(databaseName);
            builder.UseNpgsql(connectionString);
            return new ApplicationDbContext(builder.Options);
        }
    }
}