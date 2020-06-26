using System;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Отвечает за наполнение раздела с коробками в магазине.
    /// </summary>
    public class ShopLootboxService
    {
        public SectionModel Get()
        {
            throw new NotImplementedException();
            SectionModel result = new SectionModel();
            
            // result.UiItems=new SectionModel[2][];
            // //TODO добавить обычный ящик
            //
            // result.UiItems[0] = new ProductModel[1]
            // {
            //     new ProductModel()
            //     {
            //          
            //     }
            // };
            // //TODO добавить большой ящик
            
            return result;
        }
    }
}