using NetworkLibrary.NetworkLibrary.Http;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Services.Shop.ShopModel.DeleteMeShopSectionFactories
{
    public class SmallSoftCurrencyProductModelFactory
    {
        public ProductModel Create(int amount, int cost, string imagePath)
        {
            return new ProductModel()
            {
                ResourceTypeEnum = ResourceTypeEnum.SoftCurrency,
                IsDisabled = false,
                CostModel = new CostModel()
                {
                    CostTypeEnum = CostTypeEnum.HardCurrency,
                    SerializedCostModel = ZeroFormatterSerializer.Serialize(new InGameCurrencyCostModel()
                    {
                        Cost = cost
                    })
                },
                PreviewImagePath = imagePath,
                SerializedModel = ZeroFormatterSerializer.Serialize(new SoftCurrencyProductModel()
                {
                    Amount = amount
                }),
                ProductSizeEnum = ProductSizeEnum.Small
            };
        }
    }
}