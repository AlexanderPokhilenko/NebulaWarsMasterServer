using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.ShopModel.DeleteMeShopSectionFactories
{
    /// <summary>
    /// Создаёт данные для раздела хард валюты в магазне.
    /// </summary>
    public class HardCurrencySectionFactory
    {
        private readonly SmallHardCurrencyFactory factory;

        public HardCurrencySectionFactory()
        {
            factory = new SmallHardCurrencyFactory();
        }
        
        public SectionModel Create()
        {
            SectionModel sectionModel = new SectionModel
            {
                NeedFooterPointer = true,
                HeaderName = "GEM PACKS",
                SectionTypeEnum = SectionTypeEnum.HardCurrency
            };
            sectionModel.UiItems = new ProductModel[2][];
            
            //верхняя строка
            sectionModel.UiItems[0] = new[]
            {
                factory.Create(30, "diamonds5", ForeignServiceProducts.HardCurrency30),
                factory.Create(80, "diamonds10", ForeignServiceProducts.HardCurrency80),
                factory.Create(170, "diamonds15", ForeignServiceProducts.HardCurrency170)
            }; 
            
            //нижняя строка
            sectionModel.UiItems[1] = new[]
            {
                factory.Create(360, "diamonds20", ForeignServiceProducts.HardCurrency360),
                factory.Create(950, "diamonds40", ForeignServiceProducts.HardCurrency950),
                factory.Create(2000, "diamonds80", ForeignServiceProducts.HardCurrency2000),
            };
            
            return sectionModel;
        }
    }
}