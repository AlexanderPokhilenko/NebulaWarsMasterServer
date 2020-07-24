using System.Collections.Generic;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation.IncrementCreation
{
    public class SoftCurrencyIncrementsFactory:IIncrementsFactory
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
                Amount = productModel.Amount
            };
            increments.Add(increment);
            return increments;
        }
    }
}