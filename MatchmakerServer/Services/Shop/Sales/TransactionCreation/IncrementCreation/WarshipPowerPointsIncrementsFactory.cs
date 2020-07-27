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

            Console.WriteLine(model.FinishValue);
            Console.WriteLine(model.StartValue);
            Console.WriteLine(model.WarshipId);
            Console.WriteLine(model.WarshipSkinName);
            Console.WriteLine(model.MaxValueForLevel);
            
            Increment increment = new Increment
            {
                IncrementTypeId = IncrementTypeEnum.WarshipPowerPoints,
                Amount = model.FinishValue-model.StartValue,
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