using DataLayer.Entities.Transactions.Decrement;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;
using ZeroFormatter;

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
            InGameCurrencyCostModel costModel = ZeroFormatterSerializer
                .Deserialize<InGameCurrencyCostModel>(productModel.CostModel.SerializedCostModel);
            int amount = (int) costModel.Cost;
            return new Decrement
            {
                DecrementTypeId = DecrementTypeEnum.SoftCurrency,
                Amount = amount
            };
        }
    }
}