using NetworkLibrary.NetworkLibrary.Http;

namespace Code.Scenes.LobbyScene.Scripts
{
    public class SoftCurrencySectionFactory
    {
        public SectionModel Create()
        {
            SectionModel sectionModel = new SectionModel
            {
                NeedFooterPointer = false,
                HeaderName = "COIN PACKS"
            };
            sectionModel.UiItems = new ProductModel[2][];
            
            //первая строка
            sectionModel.UiItems[0] = new[]
            {
                new ProductModel
                {
                    ProductType = ProductType.SoftCurrency,
                    CurrencyType = CurrencyType.HardCurrency,
                    ImagePreviewPath = "coins5",
                    Name = "150",
                    Cost = 20.ToString(),
                    ShopItemSize = ProductSizeEnum.Small,
                    KitId = "5_1"
                }, 
                new ProductModel
                {
                    ProductType = ProductType.SoftCurrency,
                    CurrencyType = CurrencyType.HardCurrency,
                    ImagePreviewPath = "coins10",
                    Name = "400",
                    Cost = 50.ToString(),
                    ShopItemSize = ProductSizeEnum.Small,
                    KitId = "5_2"
                }
            }; 
            
            //вторая строка
            sectionModel.UiItems[1] = new[]
            {
                new ProductModel
                {
                    ProductType = ProductType.SoftCurrency,
                    CurrencyType = CurrencyType.HardCurrency,
                    ImagePreviewPath = "coins25",
                    Name = "1200",
                    Cost = 140.ToString(),
                    ShopItemSize = ProductSizeEnum.Small,
                    KitId = "5_3"
                },
                new ProductModel
                {
                    ProductType = ProductType.SoftCurrency,
                    CurrencyType = CurrencyType.HardCurrency,
                    ImagePreviewPath = "coins30",
                    Name = "2600",
                    Cost = 280.ToString(),
                    ShopItemSize = ProductSizeEnum.Small,
                    KitId = "5_4"
                }
            };
            
            return sectionModel;
        }
    }
}