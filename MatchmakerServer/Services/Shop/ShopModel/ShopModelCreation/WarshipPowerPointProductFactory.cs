using System;
using NetworkLibrary.NetworkLibrary.Http;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Services.Shop.ShopModel.ShopModelCreation
{
    public class WarshipPowerPointProductFactory
    {
        public ProductModel Create(int cost, string previewImagePath, int increment, int warshipId, 
            int maxValueForLevel, int currentAmount, WarshipTypeEnum warshipTypeEnum)
        {
            if (warshipId == 0)
            {
                throw new Exception("warshipId is empty");
            }
            
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
                SerializedModel = ZeroFormatterSerializer.Serialize(new WarshipPowerPointsProductModel()
                {
                    WarshipId = warshipId,
                    MaxValueForLevel = maxValueForLevel,
                    StartValue = currentAmount,
                    FinishValue = currentAmount+ increment,
                    WarshipSkinName = null,
                    WarshipTypeEnum =warshipTypeEnum 
                }),
                ProductSizeEnum = ProductSizeEnum.Small
            };
        }
    }
}