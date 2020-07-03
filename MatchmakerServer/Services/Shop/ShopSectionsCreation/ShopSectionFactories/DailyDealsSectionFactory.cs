using NetworkLibrary.NetworkLibrary.Http;

namespace Code.Scenes.LobbyScene.Scripts
{
    public class DailyDealsSectionFactory
    {
        public SectionModel Create()
        {
            SectionModel sectionModel = new SectionModel
            {
                HeaderName = "DAILY DEALS",
                NeedFooterPointer = true
            };
            
            sectionModel.UiItems = new ProductModel[2][];
            //первая строка
            sectionModel.UiItems[0] = new[]
            {
                new ProductModel
                {
                    ProductType = ProductType.DailyPresent,
                    CurrencyType = CurrencyType.Free,
                    ImagePreviewPath = "coins5",
                    Cost = 20.ToString(),
                    ShopItemSize = ProductSizeEnum.Small,
                    Name = "15",
                    KitId = "1_1"
                }, 
                new ProductModel
                {
                    ProductType = ProductType.WarshipPowerPoints,
                    CurrencyType = CurrencyType.SoftCurrency,
                    ImagePreviewPath = "bird",
                    Cost = 50.ToString(),
                    WarshipPowerPointsProduct = new WarshipPowerPointsProduct
                    {
                        WarshipId = 2,
                        CurrentMaxPowerPointsAmount = 12,
                        PowerPointsIncrement = 12,
                        CurrentPowerPointsAmount = 8
                    },
                    ShopItemSize = ProductSizeEnum.Small,
                    KitId = "1_2",
                    WarshipModel = new WarshipModel
                    {
                        ViewTypeId = ViewTypeId.BirdPlayer
                    }
                }, 
                new ProductModel
                {
                    ProductType = ProductType.WarshipPowerPoints,
                    CurrencyType = CurrencyType.SoftCurrency,
                    ImagePreviewPath = "bird",
                    Cost = 50.ToString(),
                    WarshipPowerPointsProduct = new WarshipPowerPointsProduct()
                    {
                        WarshipId = 2,
                        CurrentMaxPowerPointsAmount = 150,
                        PowerPointsIncrement = 16,
                        CurrentPowerPointsAmount = 48
                    },
                    ShopItemSize = ProductSizeEnum.Small,
                    KitId = "1_3",
                    WarshipModel = new WarshipModel
                    {
                        ViewTypeId = ViewTypeId.BirdPlayer
                    }
                }
            }; 
            
            //вторая строка
            sectionModel.UiItems[1] = new[]
            {
                new ProductModel
                {
                    ProductType = ProductType.WarshipPowerPoints,
                    CurrencyType = CurrencyType.SoftCurrency,
                    ImagePreviewPath = "bird",
                    Cost = 140.ToString(),
                    WarshipPowerPointsProduct = new WarshipPowerPointsProduct()
                    {
                        WarshipId = 2,
                        CurrentMaxPowerPointsAmount = 300,
                        PowerPointsIncrement = 42,
                        CurrentPowerPointsAmount = 95
                    },
                    ShopItemSize = ProductSizeEnum.Small,
                    KitId = "1_4",
                    WarshipModel = new WarshipModel
                    {
                        ViewTypeId = ViewTypeId.BirdPlayer
                    }
                },
                new ProductModel
                {
                    ProductType = ProductType.WarshipPowerPoints,
                    CurrencyType = CurrencyType.SoftCurrency,
                    ImagePreviewPath = "bird",
                    Cost = 280.ToString(),
                    WarshipPowerPointsProduct = new WarshipPowerPointsProduct()
                    {
                        WarshipId = 2,
                        CurrentMaxPowerPointsAmount = 180,
                        PowerPointsIncrement = 50,
                        CurrentPowerPointsAmount = 142
                    },
                    ShopItemSize = ProductSizeEnum.Small,
                    KitId = "1_5",
                    WarshipModel = new WarshipModel
                    {
                        ViewTypeId = ViewTypeId.BirdPlayer
                    }
                }, 
                new ProductModel
                {
                    ProductType = ProductType.WarshipPowerPoints,
                    CurrencyType = CurrencyType.SoftCurrency,
                    ImagePreviewPath = "bird",
                    Cost = 50.ToString(),
                    WarshipPowerPointsProduct = new WarshipPowerPointsProduct()
                    {
                        WarshipId = 2,
                        CurrentMaxPowerPointsAmount = 240,
                        PowerPointsIncrement = 84,
                        CurrentPowerPointsAmount = 234
                    },
                    ShopItemSize = ProductSizeEnum.Small,
                    KitId = "1_6",
                    WarshipModel = new WarshipModel
                    {
                        ViewTypeId = ViewTypeId.BirdPlayer
                    }
                }
            };
            
            return sectionModel;
        }
    }
}