using System.Collections.Generic;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation.IncrementCreation
{
    public class WarshipPowerPointsIncrementsFactory:IIncrementsFactory
    {
        public TransactionTypeEnum GetTransactionType()
        {
            return TransactionTypeEnum.WarshipPowerPoints;
        }
        
        public List<Increment> Create(ProductModel productModel)
        {
            List<Increment> increments = new List<Increment>();
            Increment increment = new Increment
            {
                IncrementTypeId = IncrementTypeEnum.WarshipPowerPoints,
                Amount = productModel.WarshipPowerPointsProduct.PowerPointsIncrement,
                WarshipId = productModel.WarshipPowerPointsProduct.WarshipId
            };
            increments.Add(increment);
            return increments;
        }
    }
}