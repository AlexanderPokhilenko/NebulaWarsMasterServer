using System.Collections.Generic;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation.IncrementCreation
{
    public class SoftCurrencyIncrementsFactory:IIncrementsFactory
    {
        public List<Increment> Create(ProductModel productModel)
        {
            List<Increment> increments = new List<Increment>();
            SoftCurrencyProductModel model = productModel;
            Increment increment = new Increment()
            {
                IncrementTypeId = IncrementTypeEnum.SoftCurrency,
                Amount = model.Amount
            };
            increments.Add(increment);
            return increments;
        }

        public ResourceTypeEnum GetResourceTypeEnum()
        {
            return ResourceTypeEnum.SoftCurrency;
        }
    }
}