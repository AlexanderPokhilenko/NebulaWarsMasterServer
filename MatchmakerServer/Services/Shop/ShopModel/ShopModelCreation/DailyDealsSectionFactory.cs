using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using DataLayer.Tables;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Создаёт модель для секции ежедневных предложений
    /// </summary>
    public class DailyDealsSectionFactory
    {
        private readonly PrizeFactoryService prizeFactoryService;
        private readonly WarshipPowerPointsProductsFactoryService wppProductsFactoryService;

        public DailyDealsSectionFactory(PrizeFactoryService prizeFactoryService,
            WarshipPowerPointsProductsFactoryService wppProductsFactoryService)
        {
            this.prizeFactoryService = prizeFactoryService;
            this.wppProductsFactoryService = wppProductsFactoryService;
        }

        public async Task<SectionModel> Create([NotNull]AccountDbDto accountDbDto)
        {
            ProductModel prizeProductModel = await prizeFactoryService.CreatePrizeProduct(accountDbDto.Id);
            List<ProductModel> warshipPowerPoints = wppProductsFactoryService
                .CreateWarshipPowerPointProducts(accountDbDto);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            foreach (ProductModel warshipPowerPoint in warshipPowerPoints)
            {
                Console.WriteLine(warshipPowerPoint.WarshipPowerPointsProduct.WarshipId);
            }
            
            
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            
            List<ProductModel> productModels = new List<ProductModel>();
            productModels.Add(prizeProductModel);
            productModels.AddRange(warshipPowerPoints);
            if (productModels.Count % 2 != 0)
            {
                throw new Exception("Нечётное кол-во элементов");
            }
            
            ProductModel[][] uiItems = new ProductModel[2][];
            uiItems[0] = productModels.Take(3).ToArray();
            uiItems[1] = productModels.TakeLast(3).ToArray();
            
            SectionModel sectionModel = new SectionModel
            {
                HeaderName = "DAILY DEALS",
                NeedFooterPointer = true,
                UiItems = uiItems
            };
            
            return sectionModel;
        }
    }
}