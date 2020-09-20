using System;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;

namespace DeleteMeDbTest
{
    public class Program
    {
        private static void Main()
        {
            string connectionString =
                "Server=main-database-1.postgres.database.azure.com;" +
                "Database=r51;" +
                "Port=5432;" +
                "User Id=database_admin@main-database-1;" +
                "Password=osj29209gf1-bkhE;" +
                "Ssl Mode=Require;";
            
            var conStrBuilder = new DbConnectionStringBuilder
            {
                {"Server", "main-database-1.postgres.database.azure.com"},
                {"Database",  "main-database-1"},
                { "Port", 5432 },
                { "User Id", "database_admin@main-database-1" },
                { "Password", "osj29209gf1-bkhE" },
                { "Ssl Mode", "Require" }
            };
            

            Console.WriteLine(connectionString);
            Console.WriteLine(conStrBuilder.ToString());
            // DbContextOptionsBuilder<ApplicationDbContext> builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            // builder.UseNpgsql(connectionString);
            // ApplicationDbContext dbContext = new ApplicationDbContext(builder.Options);
            // dbContext.Accounts.Add(new Account()
            // {
            //     Username = "asfbn",
            //     ServiceId = "asofjbna'sbnj",
            //     RegistrationDateTime = DateTime.Now
            // });
            // dbContext.SaveChanges();
            // int count = dbContext.Accounts.Count();
            // Console.WriteLine(count);
            //
            // Console.WriteLine("end");
        }
    }
}