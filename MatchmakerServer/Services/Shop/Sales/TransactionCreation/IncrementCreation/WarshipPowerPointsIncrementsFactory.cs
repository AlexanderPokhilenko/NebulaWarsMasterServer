using System;
using System.Collections.Generic;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation.IncrementCreation
{
    public class WarshipPowerPointsIncrementsFactory:IIncrementsFactory
    {
        public List<Increment> Create(ProductModel productModel)
        {
            List<Increment> increments = new List<Increment>();
            WarshipPowerPointsProductModel model = productModel;
            
            
            Console.WriteLine(model.WarshipId);
            
            
            Increment increment = new Increment
            {
                IncrementTypeId = IncrementTypeEnum.WarshipPowerPoints,
                Amount = model.Increment,
                WarshipId = model.WarshipId
            };
            increments.Add(increment);
            return increments;
        }

        public ResourceTypeEnum GetResourceTypeEnum()
        {
            return ResourceTypeEnum.WarshipPowerPoints;
        }
    }
}