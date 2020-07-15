using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation.DecrementCreation
{
    public class HardCurrencyDecrementType:IDecrementFactory
    {
        public CurrencyTypeEnum GetCurrencyTypeEnum()
        {
            return CurrencyTypeEnum.HardCurrency;
        }

        public Decrement Create(ProductModel productModel)
        {
            return new Decrement
            {
                DecrementTypeId = DecrementTypeEnum.HardCurrency,
                Amount = productModel.Cost
            };
        }
    }
}