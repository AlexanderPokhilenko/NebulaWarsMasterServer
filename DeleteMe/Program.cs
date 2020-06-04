using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;

namespace DeleteMe
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new DbContextFactory().Create("DeleteMe1");
            dbContext.Accounts.FromSql(
                new RawSqlString("ALTER DATABASE DeleteMe1 SET postgres WITH ROLLBACK IMMEDIATE"));
        }
    }
}