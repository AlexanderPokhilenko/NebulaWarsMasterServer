using DataLayer.Entities.Transactions.Decrement;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation.DecrementCreation
{
    public class HardCurrencyDecrementType:IDecrementFactory
    {
        public CostTypeEnum GetCurrencyTypeEnum()
        {
            return CostTypeEnum.HardCurrency;
        }

        public Decrement Create(ProductModel productModel)
        {
            HardCurrencyProductModel hardCurrencyProductModel = productModel;
            return new Decrement
            {
                DecrementTypeId = DecrementTypeEnum.HardCurrency,
                Amount = hardCurrencyProductModel.Amount
            };
        }
    }
}