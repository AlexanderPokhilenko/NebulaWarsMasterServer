using System;
using System.Collections.Generic;
using DataLayer.Entities.Transactions.Decrement;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer.Services.GoogleApi
{
    public class HardCurrencyTransactionFactory
    {
        public Transaction Create(int accountId, int amount, int realPurchaseModelId)
        {
            Transaction transaction = new Transaction()
            {
                DateTime = DateTime.UtcNow,
                WasShown = true,
                AccountId = accountId,
                TransactionTypeId = TransactionTypeEnum.ShopPurchase,
                Increments = new List<Increment>()
                {
                    new Increment()
                    {
                        IncrementTypeId = IncrementTypeEnum.HardCurrency,
                        Amount = amount
                    }
                },
                Decrements = new List<Decrement>()
                {
                    new Decrement()
                    {
                        DecrementTypeId = DecrementTypeEnum.RealCurrency,
                        RealPurchaseModelId = realPurchaseModelId
                    }
                }
            };
            
            return transaction;
        }
    }
}