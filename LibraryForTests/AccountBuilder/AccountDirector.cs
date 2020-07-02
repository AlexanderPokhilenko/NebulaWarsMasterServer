using System;
using System.Linq;
using DataLayer;
using DataLayer.Tables;

namespace LibraryForTests
{
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
        
        public int GetAccountRating()
        {
            int rating = Builder.GetAccount().Transactions
                    .SelectMany(transaction => transaction.Resources)
                    .SelectMany(resource =>  resource.Increments)
                    .Where(increment => increment.IncrementTypeId==IncrementTypeEnum.WarshipRating)
                    .Sum(increment => increment.Amount)
                -
                Builder.GetAccount().Transactions
                    .SelectMany(transaction => transaction.Resources)
                    .SelectMany(resource =>  resource.Decrements)
                    .Where(decrement => decrement.DecrementTypeId==DecrementTypeEnum.WarshipRating)
                    .Sum(decrement => decrement .Amount);
            return rating;
        }

        public int GetAccountSoftCurrency()
        {
            int result = Builder.GetAccount().Transactions
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Increments)
                             .Where(increment => increment.IncrementTypeId==IncrementTypeEnum.SoftCurrency)
                             .Sum(increment => increment.Amount)
                         -
                         Builder.GetAccount().Transactions
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Decrements)
                             .Where(decrement => decrement.DecrementTypeId==DecrementTypeEnum.SoftCurrency)
                             .Sum(decrement => decrement .Amount);
            return result;
        }
        
        public int GetAccountHardCurrency()
        {
            int result = Builder.GetAccount().Transactions
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Increments)
                             .Where(increment => increment.IncrementTypeId==IncrementTypeEnum.HardCurrency)
                             .Sum(increment => increment.Amount)
                         -
                         Builder.GetAccount().Transactions
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Decrements)
                             .Where(decrement => decrement.DecrementTypeId==DecrementTypeEnum.HardCurrency)
                             .Sum(decrement => decrement.Amount);
            return result;
        }

        public int GetNotShownSoftCurrencyDelta()
        {
            int result = Builder.GetAccount().Transactions
                             .Where(transaction => !transaction.WasShown)
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Increments)
                             .Where(increment => increment.IncrementTypeId==IncrementTypeEnum.SoftCurrency)
                             .Sum(increment => increment.Amount)
                      ;
            return result;
        }
        
        public int GetNotShownHardCurrencyDelta()
        {
            int result = Builder.GetAccount().Transactions
                             .Where(transaction => !transaction.WasShown)
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Increments)
                             .Where(increment => increment.IncrementTypeId==IncrementTypeEnum.HardCurrency)
                             .Sum(increment => increment.Amount)
                        ;
            return result;
        }
        
        public int GetNotShownLootboxPointsDelta()
        {
            int result = Builder.GetAccount().Transactions
                             .Where(transaction => !transaction.WasShown)
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Increments)
                             .Where(increment => increment.IncrementTypeId==IncrementTypeEnum.LootboxPoints)
                             .Sum(increment => increment.Amount)
                      ;
            return result;
        }

        public int GetWarshipRating(int warshipId)
        {
            int result = Builder.GetAccount().Transactions
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Increments)
                             .Where(increment => increment.WarshipId==warshipId)
                             .Where(increment => increment.IncrementTypeId==IncrementTypeEnum.WarshipRating)
                             .Sum(increment => increment.Amount)
                         -
                         Builder.GetAccount().Transactions
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Decrements)
                             .Where(increment => increment.WarshipId==warshipId)
                             .Where(decrement => decrement.DecrementTypeId==DecrementTypeEnum.WarshipRating)
                             .Sum(decrement => decrement.Amount);
            return result;
        }

        public int GetWarshipPowerPoints(int warshipId)
        {
            int result = Builder.GetAccount().Transactions
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Increments)
                             .Where(increment => increment.WarshipId==warshipId)
                             .Where(increment => increment.IncrementTypeId==IncrementTypeEnum.WarshipPowerPoints)
                             .Sum(increment => increment.Amount)
                       ;
            return result;
        }
      
        public int GetWarshipPowerLevel(int warshipId)
        {
            int result = Builder.GetAccount().Transactions
                    .SelectMany(transaction =>  transaction.Resources)
                    .SelectMany(resource => resource.Increments)
                    .Where(increment => increment.WarshipId==warshipId)
                    .Count(increment => increment.IncrementTypeId==IncrementTypeEnum.WarshipLevel)
                ;
            return result;
        }
        
        public int GetNotShownAccountRatingDelta()
        {
            int result = Builder.GetAccount().Transactions
                             .Where(transaction => !transaction.WasShown)
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Increments)
                             .Where(increment => increment.IncrementTypeId==IncrementTypeEnum.WarshipRating)
                             .Sum(increment => increment.Amount)
                     ;
            return result;
        }

       

        protected abstract void ConstructWarshipImprovements();
        protected abstract void ConstructWarshipLevel();

       
    }
}