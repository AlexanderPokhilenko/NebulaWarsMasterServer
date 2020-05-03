using System;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Отвечает за наполнение раздела с коробками в магазине.
    /// </summary>
    public class ShopLootboxService
    {
        public UiContainerModel Get()
        {
            throw new NotImplementedException();
            UiContainerModel result = new UiContainerModel();
            
            result.UiItems=new UiItemModel[2][];
            //TODO добавить обычный ящик
            
            result.UiItems[0] = new UiItemModel[1]
            {
                new UiItemModel()
                {
                     
                }
            };
            //TODO добавить большой ящик
            
            return result;
        }
    }
}