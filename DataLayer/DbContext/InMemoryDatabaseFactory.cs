using System;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class InMemoryDatabaseFactory:IDbContextFactory
    {
        private readonly string databaseName;
        private readonly string guidString;

        public InMemoryDatabaseFactory(string databaseName)
        {
            this.databaseName = databaseName;
            guidString = Guid.NewGuid().ToString();
        }
        
        public ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(guidString+" "+databaseName)
                .Options;
            var dbContext = new ApplicationDbContext(options);
            return dbContext;
        }
    }
}