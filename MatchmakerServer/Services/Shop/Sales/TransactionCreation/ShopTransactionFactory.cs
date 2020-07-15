using System;
using System.Collections.Generic;
using AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation.DecrementCreation;
using AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation.IncrementCreation;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation
{
    /// <summary>
    /// Создаёт транзакцию по модели продукта
    /// </summary>
    public class ShopTransactionFactory
    {
        private readonly Dictionary<CurrencyTypeEnum, Func<ProductModel, Decrement>> decrementFactories;
        private readonly Dictionary<TransactionTypeEnum, Func<ProductModel, List<Increment>>> incrementFactories;
        
        public ShopTransactionFactory(IncrementFactoriesService incrementFactoriesService,
            DecrementFactoriesService decrementFactoriesService)
        {
            incrementFactories = incrementFactoriesService.Create();
            decrementFactories = decrementFactoriesService.Create();
        }
        
        public Transaction Create(ProductModel productModel, int accountId)
        {
            if (productModel.Disabled)
            {
                throw new Exception("Этот продукт уже был куплен.");
            }

            if (!incrementFactories.ContainsKey(productModel.TransactionType))
            {
                throw new Exception("Это тип транзакции не может быть обработан " +productModel.TransactionType);
            }

            if (!decrementFactories.ContainsKey(productModel.CurrencyTypeEnum))
            {
                throw new Exception("Этот тип валюты не может быть обработан "+productModel.CurrencyTypeEnum);
            }
            
            List<Increment> increments = incrementFactories[productModel.TransactionType](productModel);
            Decrement decrement = decrementFactories[productModel.CurrencyTypeEnum](productModel);

            if (increments == null || increments.Count == 0)
            {
                throw new Exception("При покупке ничего не добавляется");
            }

            Transaction transaction = new Transaction
            {
                AccountId = accountId,
                WasShown = false,
                DateTime = DateTime.UtcNow,
                Increments = increments,
                TransactionTypeId = productModel.TransactionType,
                Decrements = new List<Decrement> {decrement}
            };
            
            return transaction;
        }
    }
}