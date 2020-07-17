using System;
using System.Collections.Generic;
using DataLayer.Entities.Transactions.Decrement;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation.DecrementCreation
{
    /// <summary>
    /// Отвечает за создание фабрик, которые по модели продукта создают декремент для транзакций
    /// </summary>
    public class DecrementFactoriesService
    {
        public Dictionary<CurrencyTypeEnum, Func<ProductModel, Decrement>> Create()
        {
            Dictionary<CurrencyTypeEnum, Func<ProductModel, Decrement>> decrementFactories =
                new Dictionary<CurrencyTypeEnum, Func<ProductModel, Decrement>>();
            
              
            List<IDecrementFactory> tmpDecrementFactories = new List<IDecrementFactory>();
            tmpDecrementFactories.Add(new SoftCurrencyDecrementType());
            tmpDecrementFactories.Add(new HardCurrencyDecrementType());
            tmpDecrementFactories.Add(new FreeDecrementType());

            foreach (IDecrementFactory decrementFactory in tmpDecrementFactories)
            {
                decrementFactories.Add(decrementFactory.GetCurrencyTypeEnum(), decrementFactory.Create);
            }

            return decrementFactories;
        }
    }
}