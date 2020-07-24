using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.ShopModel.DeleteMeShopSectionFactories
{
    public class SoftCurrencySectionFactory
    {
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
                new ProductModel
                {
                    TransactionType = TransactionTypeEnum.SoftCurrency,
                    CurrencyTypeEnum = CurrencyTypeEnum.HardCurrency,
                    ImagePreviewPath = "coins5",
                    Name = "150",
                    CostString = 20.ToString(),
                    Cost = 20,
                    ShopItemSize = ProductSizeEnum.Small,
                    Amount = 150,
                    Id = 14,
                }, 
                new ProductModel
                {
                    TransactionType = TransactionTypeEnum.SoftCurrency,
                    CurrencyTypeEnum = CurrencyTypeEnum.HardCurrency,
                    ImagePreviewPath = "coins10",
                    Name = "400",
                    CostString = 50.ToString(),
                    Cost = 50,
                    Amount = 400,
                    ShopItemSize = ProductSizeEnum.Small,
                    Id = 15,
                }
            }; 
            
            //вторая строка
            sectionModel.UiItems[1] = new[]
            {
                new ProductModel
                {
                    TransactionType = TransactionTypeEnum.SoftCurrency,
                    CurrencyTypeEnum = CurrencyTypeEnum.HardCurrency,
                    ImagePreviewPath = "coins25",
                    Name = "1200",
                    CostString = 140.ToString(),
                    Cost = 140,
                    ShopItemSize = ProductSizeEnum.Small,
                    Id = 16,
                    Amount = 1200,
                },
                new ProductModel
                {
                    TransactionType = TransactionTypeEnum.SoftCurrency,
                    CurrencyTypeEnum = CurrencyTypeEnum.HardCurrency,
                    ImagePreviewPath = "coins30",
                    Name = "2600",
                    CostString = 280.ToString(),
                    Cost = 280,
                    ShopItemSize = ProductSizeEnum.Small,
                    Id = 17,
                    Amount = 2600,
                }
            };
            
            return sectionModel;
        }
    }
}