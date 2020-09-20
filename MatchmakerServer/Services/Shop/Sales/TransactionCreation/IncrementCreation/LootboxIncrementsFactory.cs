using System.Collections.Generic;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation.IncrementCreation
{
    public class LootboxIncrementsFactory:IIncrementsFactory
    {
        public List<Increment> Create(ProductModel productModel)
        {
            List<Increment> increments = new List<Increment>();
            LootboxPointsProductModel model = productModel;
            Increment increment = new Increment
            {
                IncrementTypeId = IncrementTypeEnum.LootboxPoints,
                Amount = model.AmountOfLootboxPoints
            };
            increments.Add(increment);
            return increments;
        }

        public ResourceTypeEnum GetResourceTypeEnum()
        {
            return ResourceTypeEnum.LootboxPoints;
        }
    }
}