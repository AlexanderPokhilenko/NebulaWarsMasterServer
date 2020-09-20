using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.ShopModel.DeleteMeShopSectionFactories
{
    public class SoftCurrencySectionFactory
    {
        private SmallSoftCurrencyProductModelFactory factory;

        public SoftCurrencySectionFactory()
        {
            factory = new SmallSoftCurrencyProductModelFactory();
        }
        
        public SectionModel Create()
        {
            SectionModel sectionModel = new SectionModel
            {
                NeedFooterPointer = true,
                HeaderName = "COIN PACKS",
                SectionTypeEnum = SectionTypeEnum.SoftCurrency
            };
            sectionModel.UiItems = new ProductModel[2][];
            
            //первая строка
            sectionModel.UiItems[0] = new[]
            {
                factory.Create(150, 20, "coins5"),
                factory.Create(400, 50, "coins10")
            }; 
            
            //вторая строка
            sectionModel.UiItems[1] = new[]
            {
                factory.Create(1200, 140, "coins25"),
                factory.Create(2600, 280, "coins30")
            };
            
            return sectionModel;
        }
    }
}