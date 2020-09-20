using System;
using System.Collections.Generic;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation.IncrementCreation
{
    /// <summary>
    /// Отвечает за создание фабрик, которые по модели продукта создают инкременты для транзакции
    /// </summary>
    public class IncrementFactoryService
    {
        private readonly Dictionary<ResourceTypeEnum, Func<ProductModel, List<Increment>>> dict;

        public IncrementFactoryService()
        {
            List<IIncrementsFactory> tmpIncrements = new List<IIncrementsFactory>
            {
                new WarshipPowerPointsIncrementsFactory(),
                new LootboxIncrementsFactory(),
                new SoftCurrencyIncrementsFactory()
            };
            // tmpIncrements.Add(new LootboxSetIncrementsFactory());


            dict = new Dictionary<ResourceTypeEnum, Func<ProductModel, List<Increment>>>();
            foreach (IIncrementsFactory incrementsFactory in tmpIncrements)
            {
                dict.Add(incrementsFactory.GetResourceTypeEnum(), incrementsFactory.Create);
            }
        }
        
        public List<Increment> Create(ProductModel productModel)
        {
            if (!dict.ContainsKey(productModel.ResourceTypeEnum))
            {
                throw new Exception("Невозможно создать инкременты для этого типа ресурса");
            }

            return dict[productModel.ResourceTypeEnum](productModel);
        }
    }
}