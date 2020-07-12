using System;
using System.Linq;
using DataLayer;
using DataLayer.Tables;

namespace LibraryForTests
{
    public static class AccountExtensions
    {
        public static int GetAccountRating(this Account account)
        {
            int rating = account.Transactions
                             .SelectMany(resource =>  resource.Increments)
                             .Where(increment => increment.IncrementTypeId==IncrementTypeEnum.WarshipRating)
                             .Sum(increment => increment.Amount)
                         -
                         account.Transactions
                             .SelectMany(resource =>  resource.Decrements)
                             .Where(decrement => decrement.DecrementTypeId==DecrementTypeEnum.WarshipRating)
                             .Sum(decrement => decrement .Amount);
            return rating;
        }

        public static int GetAccountSoftCurrency(this Account account)
        {
            int result = account.Transactions
                             .SelectMany(resource =>  resource.Increments)
                             .Where(increment => increment.IncrementTypeId==IncrementTypeEnum.SoftCurrency)
                             .Sum(increment => increment.Amount)
                         -
                         account.Transactions
                             .SelectMany(resource =>  resource.Decrements)
                             .Where(decrement => decrement.DecrementTypeId==DecrementTypeEnum.SoftCurrency)
                             .Sum(decrement => decrement .Amount);
            return result;
        }
        
        public static int GetAccountHardCurrency(this Account account)
        {
            int result = account.Transactions
                             .SelectMany(resource =>  resource.Increments)
                             .Where(increment => increment.IncrementTypeId==IncrementTypeEnum.HardCurrency)
                             .Sum(increment => increment.Amount)
                         -
                         account.Transactions
                             .SelectMany(resource =>  resource.Decrements)
                             .Where(decrement => decrement.DecrementTypeId==DecrementTypeEnum.HardCurrency)
                             .Sum(decrement => decrement.Amount);
            return result;
        }

        public static int GetNotShownSoftCurrencyDelta(this Account account)
        {
            int result = account.Transactions
                             .Where(transaction => !transaction.WasShown)
                             .SelectMany(resource =>  resource.Increments)
                             .Where(increment => increment.IncrementTypeId==IncrementTypeEnum.SoftCurrency)
                             .Sum(increment => increment.Amount)
                      ;
            return result;
        }
        
        public static int GetNotShownHardCurrencyDelta(this Account account)
        {
            int result = account.Transactions
                             .Where(transaction => !transaction.WasShown)
                             .SelectMany(resource =>  resource.Increments)
                             .Where(increment => increment.IncrementTypeId==IncrementTypeEnum.HardCurrency)
                             .Sum(increment => increment.Amount)
                        ;
            return result;
        }
        
        public static int GetNotShownLootboxPointsDelta(this Account account)
        {
            int result = account.Transactions
                             .Where(transaction => !transaction.WasShown)
                             .SelectMany(resource =>  resource.Increments)
                             .Where(increment => increment.IncrementTypeId==IncrementTypeEnum.LootboxPoints)
                             .Sum(increment => increment.Amount)
                      ;
            return result;
        }

        public static int GetWarshipRating(this Account account, int warshipId)
        {
            int result = account.Transactions
                             .SelectMany(resource =>  resource.Increments)
                             .Where(increment => increment.WarshipId==warshipId)
                             .Where(increment => increment.IncrementTypeId==IncrementTypeEnum.WarshipRating)
                             .Sum(increment => increment.Amount)
                         -
                         account.Transactions
                             .SelectMany(resource =>  resource.Decrements)
                             .Where(increment => increment.WarshipId==warshipId)
                             .Where(decrement => decrement.DecrementTypeId==DecrementTypeEnum.WarshipRating)
                             .Sum(decrement => decrement.Amount);
            return result;
        }

        public static int GetWarshipPowerPoints(this Account account, int warshipId)
        {
            int result = account.Transactions
                             .SelectMany(resource =>  resource.Increments)
                             .Where(increment => increment.WarshipId==warshipId)
                             .Where(increment => increment.IncrementTypeId==IncrementTypeEnum.WarshipPowerPoints)
                             .Sum(increment => increment.Amount)
                       ;
            return result;
        }
      
        public static int GetWarshipPowerLevel(this Account account, int warshipId)
        {
            int result = account.Transactions
                    .SelectMany(resource => resource.Increments)
                    .Where(increment => increment.WarshipId==warshipId)
                    .Count(increment => increment.IncrementTypeId==IncrementTypeEnum.WarshipLevel)
                ;
            return result;
        }
        
        public static int GetNotShownAccountRatingDelta(this Account account)
        {
            int result = account.Transactions
                             .Where(transaction => !transaction.WasShown)
                             .SelectMany(resource =>  resource.Increments)
                             .Where(increment => increment.IncrementTypeId==IncrementTypeEnum.WarshipRating)
                             .Sum(increment => increment.Amount)
                     ;
            return result;
        }

       
    }
    public abstract class AccountDirector
    {
        protected readonly AccountBuilder Builder;
        private readonly ApplicationDbContext dbContext;

        protected AccountDirector(AccountBuilder builder, ApplicationDbContext dbContext)
        {
            Builder = builder;
            this.dbContext = dbContext;
        }

        public void WriteToDatabase()
        {
            Builder.SetBaseInfo("username", "serviceId", DateTime.Now);
            Account account = Builder.GetAccount();
            dbContext.Accounts.Add(account);
            dbContext.SaveChanges();
            ConstructWarships();
            dbContext.SaveChanges();
            ConstructWarshipLevel();
            dbContext.SaveChanges();
            ConstructMatches();
            dbContext.SaveChanges();
            ConstructLootboxes();
            dbContext.SaveChanges();
        }
        
        protected abstract void ConstructWarships();
        protected abstract void ConstructLootboxes();
        protected abstract void ConstructMatches();

        public Account GetAccount()
        {
            return Builder.GetAccount();
        }
   

        protected abstract void ConstructWarshipImprovements();
        protected abstract void ConstructWarshipLevel();

       
    }
}