using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using Code.Scenes.LobbyScene.Scripts;
using DataLayer.Tables;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
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

        public ShopFactoryService(AccountDbReaderService accountDbReaderService, 
            DailyDealsSectionFactory dailyDealsSectionFactory, HardCurrencySectionFactory hardCurrencySectionFactory)
        {
            this.accountDbReaderService = accountDbReaderService;
            this.dailyDealsSectionFactory = dailyDealsSectionFactory;
            this.hardCurrencySectionFactory = hardCurrencySectionFactory;
        }

        public async Task<ShopModel> Create([NotNull] string playerServiceId)
        {
            AccountDbDto accountDbDto = await accountDbReaderService.ReadAccountAsync(playerServiceId);
            if (accountDbDto == null)
            {
                throw new Exception("Игрок ещё не зарегистрирован");
            }

            ShopModel shopModel = new ShopModel
            {
                UiSections = new List<SectionModel>()
            };
            shopModel.UiSections.Add(await dailyDealsSectionFactory.Create(accountDbDto));
            shopModel.UiSections.Add(hardCurrencySectionFactory.Create());
            
                 
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