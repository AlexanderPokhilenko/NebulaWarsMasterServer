using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    public class PrizeFactoryService
    {
        private readonly ApplicationDbContext dbContext;

        public PrizeFactoryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ProductModel> CreatePrizeProduct(int accountId)
        {
            bool isPlayerRecentlyPickedUpAGift = await IsPlayerRecentlyPickedUpAGift(accountId);

            Transaction transaction = new Transaction()
            {
                AccountId = accountId,
                TransactionTypeId = TransactionTypeEnum.DailyPrize,
                DateTime = DateTime.UtcNow,
                WasShown = false,
                Increments = new List<Increment>
                {
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.SoftCurrency,
                        Amount = 15
                    }
                }
            };
            return new ProductModel
            {
                Id = 1,
                TransactionType = TransactionTypeEnum.SoftCurrency,
                CurrencyType = CurrencyType.Free,
                ImagePreviewPath = "coins5",
                ShopItemSize = ProductSizeEnum.Small,
                Name = "15",
                Disabled = isPlayerRecentlyPickedUpAGift
            };
        }
        
        private async Task<bool> IsPlayerRecentlyPickedUpAGift(int accountId)
        {
            var oneDayAgo = DateTime.UtcNow - TimeSpan.FromDays(1);
            return await dbContext.Transactions
                .Include(transaction => transaction.Account)
                .AnyAsync(transaction => transaction.AccountId== accountId 
                                         && transaction.DateTime>oneDayAgo
                                         && transaction.TransactionTypeId==TransactionTypeEnum.DailyPrize);
        }
    }
}