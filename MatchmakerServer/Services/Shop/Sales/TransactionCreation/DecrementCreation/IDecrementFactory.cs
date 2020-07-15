using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation.DecrementCreation
{
    public interface IDecrementFactory
    {
        CurrencyTypeEnum GetCurrencyTypeEnum();
        Decrement Create(ProductModel productModel);
    }
}