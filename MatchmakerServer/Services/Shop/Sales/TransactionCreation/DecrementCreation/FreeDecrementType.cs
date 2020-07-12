using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    public class FreeDecrementType:IDecrementFactory
    {
        public CurrencyTypeEnum GetCurrencyTypeEnum()
        {
            return CurrencyTypeEnum.Free;
        }

        public Decrement Create(ProductModel productModel)
        {
            return null;
        }
    }
}