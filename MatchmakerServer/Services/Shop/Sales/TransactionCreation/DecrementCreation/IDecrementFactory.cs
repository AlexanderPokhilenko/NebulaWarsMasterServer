using DataLayer.Entities.Transactions.Decrement;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation.DecrementCreation
{
    public interface IDecrementFactory
    {
        CostTypeEnum GetCurrencyTypeEnum();
        Decrement Create(ProductModel productModel);
    }
}