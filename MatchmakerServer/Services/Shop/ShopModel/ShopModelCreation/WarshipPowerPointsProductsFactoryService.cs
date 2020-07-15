using System;
using System.Collections.Generic;
using System.Linq;
using AmoebaGameMatcherServer.Experimental;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.ShopModel.ShopModelCreation
{
    public class WarshipPowerPointsProductsFactoryService
    {
        private const int NumberOfProducts = 5;
        
        /// <summary>
        /// Создаёт продукты, которые содержат улучшения для кораблей, которые есть в наличии у аккаунта
        /// </summary>
        /// <param name="accountDbDto"></param>
        /// <returns></returns>
        public List<ProductModel> CreateWarshipPowerPointProducts(AccountDbDto accountDbDto)
        {
            List<int> warshipIds = GetWarshipIds(accountDbDto);
            List<ProductModel> warshipPowerPoints = new List<ProductModel>();
            for (int index = 0; index < NumberOfProducts; index++)
            {
                int currentWarshipId = warshipIds[index];
                WarshipDbDto warshipDbDto = accountDbDto.Warships
                    .Single(dto => dto.Id == currentWarshipId);
                ProductModel wpp = new ProductModel
                {
                    TransactionType = TransactionTypeEnum.WarshipPowerPoints,
                    CurrencyTypeEnum = CurrencyTypeEnum.SoftCurrency,
                    ImagePreviewPath = warshipDbDto.WarshipType.Name.ToLower(),
                    CostString = 140.ToString(),
                    Cost = 140,
                    WarshipPowerPointsProduct = new WarshipPowerPointsProduct()
                    {
                        WarshipId = currentWarshipId,
                        CurrentMaxPowerPointsAmount = 300,
                        PowerPointsIncrement = 42,
                        CurrentPowerPointsAmount = 95
                    },
                    ShopItemSize = ProductSizeEnum.Small,
                    Id = 1654,
                };
                
                warshipPowerPoints.Add(wpp);
            }

            return warshipPowerPoints;
        }

        /// <summary>
        /// Создаёт список из id кораблей, для которых будут созданы улучшения
        /// </summary>
        /// <param name="accountDbDto"></param>
        /// <returns></returns>
        private List<int> GetWarshipIds(AccountDbDto accountDbDto)
        {
            //Обеспечит одинаковые товары для аккаунта на протяжении дня.
            int randomSeed = DateTime.UtcNow.Day;

            List<int> warshipIds = accountDbDto.Warships.Select(dto => dto.Id).ToList();
            //Если у аккаунта слишком мало кораблей, то они будут повторяться
            if (warshipIds.Count < NumberOfProducts)
            {
                Random random = new Random(randomSeed);
                int numberOfDeficientProducts = NumberOfProducts - warshipIds.Count;
                int warshipIdsCount = warshipIds.Count;
                for (int j = 0; j < numberOfDeficientProducts; j++)
                {
                    warshipIds.Add(warshipIds[random.Next(warshipIdsCount)]);
                }
            }

            warshipIds.Shuffle(randomSeed);
            return warshipIds;
        }
    }
}