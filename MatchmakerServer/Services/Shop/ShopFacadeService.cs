using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Отвечает за наполнение магазина. Наполнение магазина зависит от набора кораблей в аккаунте.
    /// </summary>
    public class ShopFacadeService
    {
        private readonly DailyDealsUiContainerService dailyDealsUiContainerService;
        private readonly ShopSkinsService shopSkinsService;
        private readonly ShopLootboxService shopLootboxService;
        private readonly ShopGemsService gemsService;
        private readonly ShopRegularCurrencyService regularCurrencyService;

        public ShopFacadeService(DailyDealsUiContainerService dailyDealsUiContainerService, 
            ShopSkinsService shopSkinsService, ShopLootboxService shopLootboxService, ShopGemsService gemsService,
            ShopRegularCurrencyService regularCurrencyService)
        {
            this.dailyDealsUiContainerService = dailyDealsUiContainerService;
            this.shopSkinsService = shopSkinsService;
            this.shopLootboxService = shopLootboxService;
            this.gemsService = gemsService;
            this.regularCurrencyService = regularCurrencyService;
        }
        
        public async Task<ShopModel> GetShopModel([NotNull] string playerServiceId)
        {
            ShopModel result = new ShopModel
            {
                UiContainerModel = new List<UiContainerModel>()
            };
            
            // UiContainerModel dailyDealsContainer = await dailyDealsUiContainerService.GetOrCreate(playerServiceId);
            // UiContainerModel skinsContainer = await shopSkinsService.GetOrCreate(playerServiceId);
            UiContainerModel lootboxContainer = shopLootboxService.Get();
            result.UiContainerModel.Add(lootboxContainer);
            // UiContainerModel gemsContainer = gemsService.Create();
            // UiContainerModel regularCurrencyContainer = regularCurrencyService.Create();

            return result;
        }
    }
}