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
                    .Sum(increment => increment.WarshipRating)
                -
                Builder.GetAccount().Transactions
                    .SelectMany(transaction => transaction.Resources)
                    .SelectMany(resource =>  resource.Decrements)
                    .Sum(decrement => decrement .WarshipRating);
            return rating;
        }

        public int GetAccountSoftCurrency()
        {
            int result = Builder.GetAccount().Transactions
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Increments)
                             .Sum(increment => increment.SoftCurrency)
                         -
                         Builder.GetAccount().Transactions
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Decrements)
                             .Sum(decrement => decrement .SoftCurrency);
            return result;
        }
        
        public int GetAccountHardCurrency()
        {
            int result = Builder.GetAccount().Transactions
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Increments)
                             .Sum(increment => increment.HardCurrency)
                         -
                         Builder.GetAccount().Transactions
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Decrements)
                             .Sum(decrement => decrement .HardCurrency);
            return result;
        }

        public int GetNotShownSoftCurrencyDelta()
        {
            int result = Builder.GetAccount().Transactions
                             .Where(transaction => !transaction.WasShown)
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Increments)
                             .Sum(increment => increment.SoftCurrency)
                         -
                         Builder.GetAccount().Transactions
                             .Where(transaction => !transaction.WasShown)
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Decrements)
                             .Sum(decrement => decrement .SoftCurrency);
            return result;
        }
        
        public int GetNotShownHardCurrencyDelta()
        {
            
            int result = Builder.GetAccount().Transactions
                             .Where(transaction => !transaction.WasShown)
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Increments)
                             .Sum(increment => increment.HardCurrency)
                         -
                         Builder.GetAccount().Transactions
                             .Where(transaction => !transaction.WasShown)
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Decrements)
                             .Sum(decrement => decrement .HardCurrency);
            return result;
        }
        
        public int GetNotShownLootboxPointsDelta()
        {
            int result = Builder.GetAccount().Transactions
                             .Where(transaction => !transaction.WasShown)
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Increments)
                             .Sum(increment => increment.LootboxPoints)
                         -
                         Builder.GetAccount().Transactions
                             .Where(transaction => !transaction.WasShown)
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Decrements)
                             .Sum(decrement => decrement.LootboxPoints);
            return result;
        }

        public int GetWarshipRating(int warshipId)
        {
            int result = Builder.GetAccount().Transactions
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Increments)
                             .Where(increment => increment.WarshipId==warshipId)
                             .Sum(increment => increment.WarshipRating)
                         -
                         Builder.GetAccount().Transactions
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Decrements)
                             .Where(increment => increment.WarshipId==warshipId)
                             .Sum(decrement => decrement.WarshipRating);
            return result;
        }

        public int GetWarshipPowerPoints(int warshipId)
        {
            int result = Builder.GetAccount().Transactions
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Increments)
                             .Where(increment => increment.WarshipId==warshipId)
                             .Sum(increment => increment.WarshipPowerPoints)
                       ;
            return result;
        }
        
        public int GetNotShownAccountRatingDelta()
        {
            int result = Builder.GetAccount().Transactions
                             .Where(transaction => !transaction.WasShown)
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Increments)
                             .Sum(increment => increment.WarshipRating)
                         -
                         Builder.GetAccount().Transactions
                             .Where(transaction => !transaction.WasShown)
                             .SelectMany(transaction => transaction.Resources)
                             .SelectMany(resource =>  resource.Decrements)
                             .Sum(decrement => decrement.WarshipRating);
            return result;
        }

        
    }
}