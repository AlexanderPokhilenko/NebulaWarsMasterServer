using System;
using System.Collections.Generic;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    public class ShopTransactionFactory
    {
        public Transaction Create(ProductModel productModel, int accountId)
        {
            if (productModel.Disabled)
            {
                throw new Exception("Этот продукт уже был куплен.");
            }
            
            Transaction transaction = new Transaction();
            transaction.AccountId = accountId;
            transaction.DateTime = DateTime.UtcNow;
            transaction.WasShown = false;
            transaction.TransactionTypeId = productModel.TransactionType;

            List<Resource> resources = CreateResources(productModel);
            if (resources == null || resources.Count == 0)
            {
                throw new Exception("Не удалось создать ресурсы по модели продукта.");
            }
            

            return transaction;
        }

        private List<Resource> CreateResources(ProductModel productModel)
        {
            List<Resource> result = new List<Resource>();
            // re
            return result;
        }
    }
}