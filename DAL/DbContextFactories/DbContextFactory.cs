using Microsoft.EntityFrameworkCore;

namespace DataLayer.DbContextFactories
{
    /// <summary>
    /// Создаёт подключение к БД по connectionString
    /// </summary>
    public class DbContextFactory:IDbContextFactory
    {
        private readonly IDbConnectionConfig dbConnectionConfig;

        public DbContextFactory(IDbConnectionConfig dbConnectionConfig)
        {
            this.dbConnectionConfig = dbConnectionConfig;
        }
        
        public ApplicationDbContext Create(string databaseName = null)
        {
            DbContextOptionsBuilder<ApplicationDbContext> builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            string connectionString = dbConnectionConfig.GetConnectionString();
            builder.UseNpgsql(connectionString);
            return new ApplicationDbContext(builder.Options);
        }
    }
}