using System;
using NetworkLibrary.NetworkLibrary.Http;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Services.Shop.ShopModel.ShopModelCreation
{
    public class WarshipPowerPointProductFactory
    {
        public ProductModel Create(int cost, string previewImagePath, int increment, int warshipId, 
            WarshipTypeEnum warshipTypeEnum)
        {
            if (warshipId == 0)
            {
                throw new Exception("warshipId is empty");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Создание очков силы для корабля:");
            Console.WriteLine($"{nameof(warshipId)} {warshipId} " +
                              $"{nameof(increment)} {increment} " +
                              $"{nameof(warshipTypeEnum)} {warshipTypeEnum}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
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
                SerializedModel = ZeroFormatterSerializer.Serialize(new WarshipPowerPointsProductModel
                {
                    WarshipId = warshipId,
                    Increment = increment,
                    WarshipTypeEnum = warshipTypeEnum,
                    SupportClientModel = null
                }),
                ProductSizeEnum = ProductSizeEnum.Small
            };
        }
    }
}