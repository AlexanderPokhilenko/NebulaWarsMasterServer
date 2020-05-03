using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Отвечает за наполнение меню с ежедневными скидками.
    /// </summary>
    public class DailyDealsUiContainerService
    {
        private readonly ShopFreeGiftManagerService shopFreeGiftManagerService;
        private readonly ShopWarshipPowerImprovementService warshipPowerImprovementService;

        public DailyDealsUiContainerService(ShopFreeGiftManagerService shopFreeGiftManagerService, 
            ShopWarshipPowerImprovementService warshipPowerImprovementService)
        {
            this.shopFreeGiftManagerService = shopFreeGiftManagerService;
            this.warshipPowerImprovementService = warshipPowerImprovementService;
        }
        
        public async Task<UiContainerModel> GetOrCreate([NotNull] string playerServiceId)
        {
            var freeGift = await shopFreeGiftManagerService.GetOrCreate(playerServiceId);
            var warshipsImprovements = await warshipPowerImprovementService
                .GetOrCreate(playerServiceId, 5);
            
            throw new NotImplementedException();
        }
    }
}