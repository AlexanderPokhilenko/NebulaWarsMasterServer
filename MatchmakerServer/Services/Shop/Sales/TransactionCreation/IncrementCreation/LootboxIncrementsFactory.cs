using System.Collections.Generic;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation.IncrementCreation
{
    public class LootboxIncrementsFactory:IIncrementsFactory
    {
        public TransactionTypeEnum GetTransactionType()
        {
            return TransactionTypeEnum.Lootbox;
        }
        
        public List<Increment> Create(ProductModel productModel)
        {
            List<Increment> increments = new List<Increment>();
            Increment increment = new Increment
            {
                IncrementTypeId = IncrementTypeEnum.WarshipPowerPoints,
                Amount = productModel.Amount,
                WarshipId = productModel.WarshipPowerPointsProduct.WarshipId
            };
            increments.Add(increment);
            return increments;
        }
    } public class SoftCurrencyIncrementsFactory:IIncrementsFactory
    {
        public TransactionTypeEnum GetTransactionType()
        {
            return TransactionTypeEnum.SoftCurrency;
        }
        
        public List<Increment> Create(ProductModel productModel)
        {
            List<Increment> increments = new List<Increment>();
            Increment increment = new Increment()
            {
                IncrementTypeId = IncrementTypeEnum.SoftCurrency,
                Amount = productModel.Amount,
                WarshipId = productModel.WarshipModel.WarshipId
            };
            increments.Add(increment);
            return increments;
        }
    }
}