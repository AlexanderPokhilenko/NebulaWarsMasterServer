using DataLayer.Entities.Transactions.Decrement;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation.DecrementCreation
{
    public class SoftCurrencyDecrementType:IDecrementFactory
    {
        public CostTypeEnum GetCurrencyTypeEnum()
        {
            return CostTypeEnum.SoftCurrency;
        }

        public Decrement Create(ProductModel productModel)
        {
            SoftCurrencyProductModel model = productModel;
            return new Decrement
            {
                DecrementTypeId = DecrementTypeEnum.SoftCurrency,
                Amount = model.Amount
            };
        }
    }
}