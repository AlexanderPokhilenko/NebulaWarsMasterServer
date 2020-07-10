using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace Code.Scenes.LobbyScene.Scripts
{
    public static class ForeignServiceProducts
    {
        public const string HardCurrency30 = "hard_currency_30";
        public const string HardCurrency80 = "hard_currency_80";
        public const string HardCurrency170 = "hard_currency_170";
        public const string HardCurrency360 = "hard_currency_360";
        public const string HardCurrency950 = "hard_currency_950";
        public const string HardCurrency2000 = "hard_currency_2000";
    }
    
    /// <summary>
    /// Создаёт данные для раздела хард валюты в магазне.
    /// </summary>
    public class HardCurrencySectionFactory
    {
        public SectionModel Create()
        {
            SectionModel sectionModel = new SectionModel
            {
                NeedFooterPointer = true,
                HeaderName = "GEM PACKS"
            };
            sectionModel.UiItems = new ProductModel[2][];
            
            //верхняя строка
            sectionModel.UiItems[0] = new[]
            {
                new ProductModel
                {
                    TransactionType = TransactionTypeEnum.HardCurrency,
                    CurrencyType = CurrencyType.RealCurrency,
                    ImagePreviewPath = "diamonds5",
                    ForeignServiceProduct = new ForeignServiceProduct
                    {
                        ProductGoogleId = ForeignServiceProducts.HardCurrency30,
                        Consumable = true
                    },
                    Name = "30",
                    ShopItemSize = ProductSizeEnum.Small,
                    Id = 3
                }, 
                new ProductModel
                {
                    TransactionType = TransactionTypeEnum.HardCurrency,
                    CurrencyType = CurrencyType.RealCurrency,
                    ImagePreviewPath = "diamonds10",
                    ForeignServiceProduct = new ForeignServiceProduct
                    {
                        ProductGoogleId = ForeignServiceProducts.HardCurrency80,
                        Consumable = true
                    },
                    Name = "80",
                    ShopItemSize = ProductSizeEnum.Small,
                    Id = 4
                },
                new ProductModel
                {
                    TransactionType = TransactionTypeEnum.HardCurrency,
                    CurrencyType = CurrencyType.RealCurrency,
                    ImagePreviewPath = "diamonds15",
                    ForeignServiceProduct = new ForeignServiceProduct
                    {
                        ProductGoogleId = ForeignServiceProducts.HardCurrency170,
                        Consumable = true
                    },
                    Name = "170",
                    ShopItemSize = ProductSizeEnum.Small,
                    Id = 5
                }
            }; 
            
            //нижняя строка
            sectionModel.UiItems[1] = new[]
            {
                new ProductModel
                {
                    TransactionType = TransactionTypeEnum.HardCurrency,
                    CurrencyType = CurrencyType.RealCurrency,
                    ImagePreviewPath = "diamonds20",
                    ForeignServiceProduct = new ForeignServiceProduct
                    {
                        ProductGoogleId = ForeignServiceProducts.HardCurrency360,
                        Consumable = true
                    },
                    Name = "360",
                    ShopItemSize = ProductSizeEnum.Small,
                    Id = 5
                },
                new ProductModel
                {
                    TransactionType = TransactionTypeEnum.HardCurrency,
                    CurrencyType = CurrencyType.RealCurrency,
                    ImagePreviewPath = "diamonds40",
                    ForeignServiceProduct = new ForeignServiceProduct
                    {
                        ProductGoogleId = ForeignServiceProducts.HardCurrency950,
                        Consumable = true
                    },
                    Name = "950",
                    ShopItemSize = ProductSizeEnum.Small,
                    Id = 6
                },
                new ProductModel
                {
                    TransactionType = TransactionTypeEnum.HardCurrency,
                    CurrencyType = CurrencyType.RealCurrency,
                    ImagePreviewPath = "diamonds80",
                    ForeignServiceProduct = new ForeignServiceProduct
                    {
                        ProductGoogleId = ForeignServiceProducts.HardCurrency2000,
                        Consumable = true
                    },
                    Name = "2000",
                    ShopItemSize = ProductSizeEnum.Small,
                    Id = 7
                }
            };
            
            return sectionModel;
        }
    }
}