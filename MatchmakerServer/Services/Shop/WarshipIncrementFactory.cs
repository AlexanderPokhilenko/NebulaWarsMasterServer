using System.Collections.Generic;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    public class WarshipIncrementFactory:IIncrementsFactory
    {
        public TransactionTypeEnum GetTransactionType()
        {
            return TransactionTypeEnum.Warship;
        }
        
        public List<Increment> Create(ProductModel productModel)
        {
            List<Increment> increments = new List<Increment>();
            Increment increment = new Increment
            {
                IncrementTypeId = IncrementTypeEnum.Warship,
                Amount = 1,
                WarshipId = productModel.WarshipModel.WarshipId
            };
            increments.Add(increment);
            return increments;
        }
    }
}