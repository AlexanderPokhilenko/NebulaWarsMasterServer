using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace Code.Scenes.LobbyScene.Scripts
{
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
                        ProductGoogleId = "hard_currency_30"
                    },
                    Name = "30",
                    ShopItemSize = ProductSizeEnum.Small,
                    Id = 3,
                }, 
                new ProductModel
                {
                    TransactionType = TransactionTypeEnum.HardCurrency,
                    CurrencyType = CurrencyType.RealCurrency,
                    ImagePreviewPath = "diamonds10",
                    ForeignServiceProduct = new ForeignServiceProduct
                    {
                        ProductGoogleId = "hard_currency_80"
                    },
                    Name = "80",
                    ShopItemSize = ProductSizeEnum.Small,
                    Id = 4,
                },
                new ProductModel
                {
                    TransactionType = TransactionTypeEnum.HardCurrency,
                    CurrencyType = CurrencyType.RealCurrency,
                    ImagePreviewPath = "diamonds15",
                    ForeignServiceProduct = new ForeignServiceProduct
                    {
                        ProductGoogleId = "hard_currency_170"
                    },
                    Name = "170",
                    ShopItemSize = ProductSizeEnum.Small,
                    Id = 5,
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
                        ProductGoogleId = "hard_currency_360"
                    },
                    Name = "360",
                    ShopItemSize = ProductSizeEnum.Small,
                    Id = 5,
                },
                new ProductModel
                {
                    TransactionType = TransactionTypeEnum.HardCurrency,
                    CurrencyType = CurrencyType.RealCurrency,
                    ImagePreviewPath = "diamonds40",
                    ForeignServiceProduct = new ForeignServiceProduct
                    {
                        ProductGoogleId = "hard_currency_950"
                    },
                    Name = "950",
                    ShopItemSize = ProductSizeEnum.Small,
                    Id = 6,
                },
                new ProductModel
                {
                    TransactionType = TransactionTypeEnum.HardCurrency,
                    CurrencyType = CurrencyType.RealCurrency,
                    ImagePreviewPath = "diamonds80",
                    ForeignServiceProduct = new ForeignServiceProduct
                    {
                        ProductGoogleId = "hard_currency_2000"
                    },
                    Name = "2000",
                    ShopItemSize = ProductSizeEnum.Small,
                    Id = 7,
                }
            };
            
            return sectionModel;
        }
    }
}