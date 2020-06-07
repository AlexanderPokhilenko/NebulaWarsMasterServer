using System;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Отвечает за наполнение раздела с обычной валютой в магазине.
    /// Цена наборов зависит от гугла.
    /// Тут определяется количество наборов и из размер.
    /// </summary>
    public class ShopRegularCurrencyService
    {
        public ShopSectionModel Get()
        {
            ShopSectionModel model = new ShopSectionModel();
            //TODO добавить обычную валюту
            throw new NotImplementedException();
        }
    }
}