using System;
using System.Collections.Generic;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation.IncrementCreation
{
    /// <summary>
    /// Отвечает за создание фабрик, которые по модели продукта создают инкременты для транзакции
    /// </summary>
    public class IncrementFactoriesService
    {
        public Dictionary<TransactionTypeEnum, Func<ProductModel, List<Increment>>> Create()
        {
            Dictionary<TransactionTypeEnum, Func<ProductModel, List<Increment>>> dict = 
                new Dictionary<TransactionTypeEnum, Func<ProductModel, List<Increment>>>();
            List<IIncrementsFactory> tmpIncrements = new List<IIncrementsFactory>();
            
            tmpIncrements.Add(new WarshipPowerPointsIncrementsFactory());
            tmpIncrements.Add(new LootboxIncrementsFactory());
            tmpIncrements.Add(new LootboxSetIncrementsFactory());
            tmpIncrements.Add(new SoftCurrencyIncrementsFactory());

            foreach (IIncrementsFactory incrementsFactory in tmpIncrements)
            {
                dict.Add(incrementsFactory.GetTransactionType(), incrementsFactory.Create);
            }

            return dict;
        }
    }
}