using System.Collections.Generic;
using NetworkLibrary.NetworkLibrary.Http;

namespace Code.Scenes.LobbyScene.Scripts
{
    /// <summary>
    /// Поэтапно создаёт разделы в магазине.
    /// </summary>
    public class ShopModelBuilder
    {
        public ShopModel Create()
        {
            ShopModel shopModel = new ShopModel
            {
                UiSections = new List<SectionModel>()
            };
            shopModel.UiSections.Add(new DailyDealsSectionFactory().Create());
            shopModel.UiSections.Add(new SkinsSectionFactory().Create());
            shopModel.UiSections.Add(new WarshipsSectionFactory().Create());
            shopModel.UiSections.Add(new LootboxSectionFactory().Create());
            shopModel.UiSections.Add(new HardCurrencySectionFactory().Create());
            shopModel.UiSections.Add(new SoftCurrencySectionFactory().Create());
            return shopModel;
        }
    }
}