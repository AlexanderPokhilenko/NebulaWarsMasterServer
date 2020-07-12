using System.Collections.Generic;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    public interface IIncrementsFactory
    {
        TransactionTypeEnum GetTransactionType();
        List<Increment> Create(ProductModel productModel);
    }
}