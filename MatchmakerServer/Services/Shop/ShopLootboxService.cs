using System;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Отвечает за наполнение раздела с коробками в магазине.
    /// </summary>
    public class ShopLootboxService
    {
        public ShopSectionModel Get()
        {
            throw new NotImplementedException();
            ShopSectionModel result = new ShopSectionModel();
            
            result.UiItems=new ShopItemModel[2][];
            //TODO добавить обычный ящик
            
            result.UiItems[0] = new ShopItemModel[1]
            {
                new ShopItemModel()
                {
                     
                }
            };
            //TODO добавить большой ящик
            
            return result;
        }
    }
}