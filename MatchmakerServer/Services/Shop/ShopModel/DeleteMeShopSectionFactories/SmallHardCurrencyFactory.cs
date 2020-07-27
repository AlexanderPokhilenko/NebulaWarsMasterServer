using NetworkLibrary.NetworkLibrary.Http;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Services.Shop.ShopModel.DeleteMeShopSectionFactories
{
    public class SmallHardCurrencyFactory
    {
        public ProductModel Create(int amount, string imagePath, string productGoogleId)
        {
            return new ProductModel
            {
                ResourceTypeEnum = ResourceTypeEnum.HardCurrency,
                CostModel = new CostModel()
                {
                    CostTypeEnum = CostTypeEnum.RealCurrency,
                    SerializedCostModel = ZeroFormatterSerializer.Serialize(new RealCurrencyCostModel()
                    {
                        IsConsumable = true,
                        AppleProductId = null,
                        GoogleProductId = productGoogleId,
                        CostString = null
                    })
                },
                IsDisabled = false,
                SerializedModel = ZeroFormatterSerializer.Serialize(new HardCurrencyProductModel()
                {
                    Amount = amount
                }),
                PreviewImagePath = imagePath,
                ProductSizeEnum = ProductSizeEnum.Small
            };
        }
    }
}