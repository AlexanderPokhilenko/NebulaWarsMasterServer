using System;
using System.Collections.Generic;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation.IncrementCreation
{
    public class LootboxSetIncrementsFactory:IIncrementsFactory
    {
        public TransactionTypeEnum GetTransactionType()
        {
            return TransactionTypeEnum.LootboxSet;
        }
        
        public List<Increment> Create(ProductModel productModel)
        {
            List<Increment> increments = new List<Increment>();
            if (productModel.MagnificationRatio != null)
            {
                Increment increment = new Increment
                {
                    IncrementTypeId = IncrementTypeEnum.LootboxPoints,
                    Amount = 100*productModel.MagnificationRatio.Value
                };
                increments.Add(increment);
            }
            else
            {
                throw new Exception("Набор лутбоксов не содержит кол-ва лутбоксов");
            }
            return increments;
        }
    }
}