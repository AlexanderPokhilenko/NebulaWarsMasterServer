using System.Linq;
using AmoebaGameMatcherServer.Experimental;
using DataLayer;

namespace AmoebaGameMatcherServer.Services.Database.Seeding.Seaders
{
    public class AccountSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            if (!dbContext.Accounts.Any())
            {
                var service = new DefaultAccountFactoryService(dbContext);
                service.CreateDefaultAccountAsync("serviceId").Wait();
                
            }

        }
    }
}