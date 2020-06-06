using System;
using System.Linq;
using AmoebaGameMatcherServer;
using DataLayer;
using DataLayer.Tables;
using LibraryForTests;

namespace DeleteMe1
{
    public class DbSeed
    {
        private readonly ApplicationDbContext dbContext;

        public DbSeed(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public void TryInsert()
        {
            try
            {
                new DataSeeder().TrySeed(dbContext);
                AccountBuilder builder = new AccountBuilder();
                AccountDirector accountDirector = new SmallAccountDirector(builder, dbContext);
                accountDirector.WriteToDatabase();
                Account account = accountDirector.GetAccount();
                dbContext.Accounts.Add(account);
                dbContext.SaveChanges();
            }
            catch (AggregateException e)
            {
                Console.WriteLine("Не удалось всавить данные");
                Console.WriteLine(e.Message+e.InnerExceptions);
            }
        }

        public string GetSomeServiceId()
        {
            return dbContext.Accounts.FirstOrDefault()?.ServiceId;
        }
    }
}