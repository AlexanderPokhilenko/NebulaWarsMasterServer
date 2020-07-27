using System;
using System.Collections.Generic;
using AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation.DecrementCreation;
using AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation.IncrementCreation;
using DataLayer.Entities.Transactions.Decrement;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation
{
    /// <summary>
    /// Создаёт транзакцию по модели продукта
    /// </summary>
    public class ShopTransactionFactory
    {
        private readonly IncrementFactoryService incrementFactoryService;
        private readonly DecrementFactoryService decrementFactoryService;

        public ShopTransactionFactory(IncrementFactoryService incrementFactoryService,
            DecrementFactoryService decrementFactoryService)
        {
            this.incrementFactoryService = incrementFactoryService;
            this.decrementFactoryService = decrementFactoryService;
        }
        
        public Transaction Create(ProductModel productModel, int accountId)
        {
            if (productModel.IsDisabled)
            {
                throw new Exception("Этот продукт недоступен для покупки.");
            }

            List<Increment> increments = incrementFactoryService.Create(productModel);
            Decrement decrement = decrementFactoryService.Create(productModel);
            Transaction transaction = new Transaction
            {
                AccountId = accountId,
                WasShown = true,
                DateTime = DateTime.UtcNow,
                TransactionTypeId = TransactionTypeEnum.ShopPurchase,
                Increments = increments,
                Decrements = new List<Decrement> {decrement}
            };
            
            return transaction;
        }
    }
}