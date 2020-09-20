using System;
using System.Collections.Generic;
using System.Linq;
using AmoebaGameMatcherServer.Experimental;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Services.Shop.ShopModel.ShopModelCreation
{
    public class TmpWarshipDich
    {
        public int warshipId;
        public WarshipTypeEnum warshipType;
        
    }
    public class WarshipPowerPointsProductsFactoryService
    {
        private const int NumberOfProducts = 6;
        private readonly WarshipPowerPointProductFactory factory;

        public WarshipPowerPointsProductsFactoryService()
        {
            factory = new WarshipPowerPointProductFactory();
        }
        
        /// <summary>
        /// Создаёт продукты, которые содержат улучшения для кораблей, которые есть в наличии у аккаунта
        /// </summary>
        /// <param name="accountDbDto"></param>
        /// <returns></returns>
        public List<ProductModel> CreateWarshipPowerPointProducts(AccountDbDto accountDbDto)
        {
            List<TmpWarshipDich> warshipIds = GetWarshipModels(accountDbDto);
            List<ProductModel> warshipPowerPoints = new List<ProductModel>();
            for (int index = 0; index < NumberOfProducts; index++)
            {
                int warshipId = warshipIds[index].warshipId;
                WarshipTypeEnum warshipType = warshipIds[index].warshipType;
                
                WarshipDbDto warshipDbDto = accountDbDto.Warships
                    .Single(dto => dto.Id == warshipId);
                string previewPath = warshipDbDto.WarshipType.Name.ToLower();
                ProductModel wpp = factory.Create(140, previewPath, 42, warshipId, warshipType);
                warshipPowerPoints.Add(wpp);
            }

            foreach (var productModel in warshipPowerPoints)
            {
                WarshipPowerPointsProductModel model = productModel;
            }

            return warshipPowerPoints;
        }

        /// <summary>
        /// Создаёт список из id кораблей, для которых будут созданы улучшения
        /// </summary>
        /// <param name="accountDbDto"></param>
        /// <returns></returns>
        private List<TmpWarshipDich> GetWarshipModels(AccountDbDto accountDbDto)
        {
            //Обеспечит одинаковые товары для аккаунта на протяжении дня.
            int randomSeed = DateTime.UtcNow.Day;

            List<TmpWarshipDich> warshipIds = accountDbDto.Warships.Select(dto => new TmpWarshipDich()
            {
                warshipId = dto.Id,
                warshipType = dto.WarshipTypeId
            } ).ToList();
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