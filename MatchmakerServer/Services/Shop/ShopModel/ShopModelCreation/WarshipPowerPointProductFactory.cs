using NetworkLibrary.NetworkLibrary.Http;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Services.Shop.ShopModel.ShopModelCreation
{
    public class WarshipPowerPointProductFactory
    {
        public ProductModel Create(int cost, string previewImagePath, int increment, int warshipId, int maxValueForLevel, int currentAmount)
        {
            return new ProductModel
            {
                ResourceTypeEnum = ResourceTypeEnum.WarshipPowerPoints,
                CostModel = new CostModel()
                {
                    CostTypeEnum = CostTypeEnum.SoftCurrency,
                    SerializedCostModel = ZeroFormatterSerializer.Serialize(
                        new InGameCurrencyCostModel()
                        {
                            Cost = cost
                        })
                },
                PreviewImagePath = previewImagePath,
                SerializedModel = ZeroFormatterSerializer.Serialize(new WarshipPowerPointsProduct()
                {
                    WarshipId = warshipId,
                    CurrentMaxPowerPointsAmount = maxValueForLevel,
                    PowerPointsIncrement = increment,
                    CurrentPowerPointsAmount = currentAmount
                }),
                ProductSizeEnum = ProductSizeEnum.Small
            };
        }
    }
}