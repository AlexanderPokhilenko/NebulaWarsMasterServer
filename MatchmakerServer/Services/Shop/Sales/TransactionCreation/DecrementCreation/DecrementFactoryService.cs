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
    public class DecrementFactoryService
    {
        private readonly Dictionary<CostTypeEnum, Func<ProductModel, Decrement>> decrementFactories;

        public DecrementFactoryService()
        {
            decrementFactories = new Dictionary<CostTypeEnum, Func<ProductModel, Decrement>>();

            List<IDecrementFactory> tmpDecrementFactories = new List<IDecrementFactory>
            {
                new SoftCurrencyDecrementType(), new HardCurrencyDecrementType(), new FreeDecrementType()
            };

            foreach (IDecrementFactory decrementFactory in tmpDecrementFactories)
            {
                decrementFactories.Add(decrementFactory.GetCurrencyTypeEnum(), decrementFactory.Create);
            }
        }
        
        public Decrement Create(ProductModel productModel)
        {
            if (!decrementFactories.ContainsKey(productModel.CostModel.CostTypeEnum))
            {
                throw new Exception("Невозможно создать декремент по модели продукта. "
                                    +productModel.CostModel.CostTypeEnum);
            }

            return decrementFactories[productModel.CostModel.CostTypeEnum](productModel);
        }
    }
}