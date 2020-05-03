using System;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Отвечает за наполнение раздела с премиум валютой в магазине.
    /// Цена наборов с премиум-валютой зависит от настроек гугла.
    /// Тут определяется размер наборов и их количество. 
    /// </summary>
    public class ShopGemsService
    {
        public UiContainerModel Get()
        {
            UiContainerModel model = new UiContainerModel();
            //TODO добавить премиум валюту 
            throw new NotImplementedException();
        }
    }
}