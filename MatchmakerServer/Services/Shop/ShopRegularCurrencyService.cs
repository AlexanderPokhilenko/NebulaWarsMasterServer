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
        public SectionModel Get()
        {
            SectionModel model = new SectionModel();
            //TODO добавить обычную валюту
            throw new NotImplementedException();
        }
    }
}