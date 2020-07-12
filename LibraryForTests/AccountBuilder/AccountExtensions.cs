    using System.Collections.Generic;
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

        public static void AddLootbox(this Account account, ApplicationDbContext dbContext)
        {
            Transaction transaction = new Transaction()
            {
                TransactionTypeId = TransactionTypeEnum.Lootbox,
                Increments = new List<Increment>()
                {
                    new Increment()
                    {
                        IncrementTypeId = IncrementTypeEnum.LootboxPoints,
                        Amount = 100
                    }
                },
                Decrements = new List<Decrement>()
                {
                    new Decrement()
                    {
                        DecrementTypeId = DecrementTypeEnum.HardCurrency,
                        Amount = 80
                    }
                }
            };
            account.Transactions.Add(transaction);
            dbContext.SaveChanges();
        }
    }
}