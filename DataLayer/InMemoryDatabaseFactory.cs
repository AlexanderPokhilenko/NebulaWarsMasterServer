using System;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public static class InMemoryDatabaseFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var dbContext = new ApplicationDbContext(options);
            return dbContext;
        }
    }
}