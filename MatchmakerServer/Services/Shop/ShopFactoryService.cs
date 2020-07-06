using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using DataLayer.Tables;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    public class ShopFactoryService
    {
        private readonly AccountDbReaderService accountDbReaderService;
        private readonly DailyDealsSectionFactory dailyDealsSectionFactory;

        public ShopFactoryService(AccountDbReaderService accountDbReaderService, DailyDealsSectionFactory dailyDealsSectionFactory)
        {
            this.accountDbReaderService = accountDbReaderService;
            this.dailyDealsSectionFactory = dailyDealsSectionFactory;
        }

        public async Task<ShopModel> Create([NotNull] string playerServiceId)
        {
            AccountDbDto accountDbDto = await accountDbReaderService.ReadAccountAsync(playerServiceId);
            if (accountDbDto == null)
            {
                throw new Exception("Игрок ещё не зарегистрирован");
            }

            ShopModel shopModel = new ShopModel()
            {
                UiSections = new List<SectionModel>()
            };
            shopModel.UiSections.Add(await dailyDealsSectionFactory.Create(accountDbDto));
            
                 
            // shopModel.UiSections.Add(new SkinsSectionFactory().Create());
            // shopModel.UiSections.Add(new WarshipsSectionFactory().Create());
            // shopModel.UiSections.Add(new LootboxSectionFactory().Create());
            // shopModel.UiSections.Add(new HardCurrencySectionFactory().Create());
            // shopModel.UiSections.Add(new SoftCurrencySectionFactory().Create());
            return shopModel;
        }
    }
}