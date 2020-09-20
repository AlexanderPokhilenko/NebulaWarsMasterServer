using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using AmoebaGameMatcherServer.Services.Shop.ShopModel.DeleteMeShopSectionFactories;
using DataLayer.Tables;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.ShopModel.ShopModelCreation
{
    /// <summary>
    /// Отвечает за создание модели магазина.
    /// Модель зависит от:
    /// 1) Набора кораблей игрока, их прокачки
    /// 2) От того, на каких кораблях игрок играет чаще всего.
    /// 3) От предыдущих покупок игрока.
    /// </summary>
    public class ShopFactoryService
    {
        private readonly AccountDbReaderService accountDbReaderService;
        private readonly DailyDealsSectionFactory dailyDealsSectionFactory;
        private readonly HardCurrencySectionFactory hardCurrencySectionFactory;
        private readonly SoftCurrencySectionFactory softCurrencySectionFactory;

        public ShopFactoryService(AccountDbReaderService accountDbReaderService, 
            DailyDealsSectionFactory dailyDealsSectionFactory, HardCurrencySectionFactory hardCurrencySectionFactory,
            SoftCurrencySectionFactory softCurrencySectionFactory)
        {
            this.accountDbReaderService = accountDbReaderService;
            this.dailyDealsSectionFactory = dailyDealsSectionFactory;
            this.hardCurrencySectionFactory = hardCurrencySectionFactory;
            this.softCurrencySectionFactory = softCurrencySectionFactory;
        }

        public async Task<NetworkLibrary.NetworkLibrary.Http.ShopModel> Create([NotNull] string playerServiceId)
        {
            AccountDbDto accountDbDto = await accountDbReaderService.ReadAccountAsync(playerServiceId);
            if (accountDbDto == null)
            {
                throw new Exception("Игрок ещё не зарегистрирован");
            }

            NetworkLibrary.NetworkLibrary.Http.ShopModel shopModel = new NetworkLibrary.NetworkLibrary.Http.ShopModel
            {
                UiSections = new List<SectionModel>()
            };
            shopModel.UiSections.Add(await dailyDealsSectionFactory.Create(accountDbDto));
            shopModel.UiSections.Add(hardCurrencySectionFactory.Create());
            shopModel.UiSections.Add(softCurrencySectionFactory.Create());
            
                 
            // shopModel.UiSections.Add(new SkinsSectionFactory().Create());
            // shopModel.UiSections.Add(new WarshipsSectionFactory().Create());
            // shopModel.UiSections.Add(new LootboxSectionFactory().Create());
            // shopModel.UiSections.Add(new SoftCurrencySectionFactory().Create());
            
            //Присвоить продуктам уникальные id
            int startIndex = 1;
            foreach (ProductModel productModel in shopModel.UiSections
                .SelectMany(section=>section.UiItems)
                .SelectMany(item=>item))
            {
                productModel.Id = startIndex++;
            }
            
            
            return shopModel;
        }
    }
}