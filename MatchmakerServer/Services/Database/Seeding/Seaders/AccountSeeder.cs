using System;
using System.Collections.Generic;
using System.Linq;
using AmoebaGameMatcherServer.Services;
using DataLayer;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer
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