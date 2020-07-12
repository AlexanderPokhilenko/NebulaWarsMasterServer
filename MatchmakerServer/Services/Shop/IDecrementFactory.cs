using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    public interface IDecrementFactory
    {
        CurrencyTypeEnum GetCurrencyTypeEnum();
        Decrement Create(ProductModel productModel);
    }
}