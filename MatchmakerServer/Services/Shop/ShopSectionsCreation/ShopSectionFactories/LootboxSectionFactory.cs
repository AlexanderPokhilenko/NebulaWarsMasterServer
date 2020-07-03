using NetworkLibrary.NetworkLibrary.Http;

namespace Code.Scenes.LobbyScene.Scripts
{
    public class LootboxSectionFactory
    {
        public SectionModel Create()
        {
            SectionModel sectionModel = new SectionModel
            {
                NeedFooterPointer = true,
                HeaderName = "BOXES"
            };
            sectionModel.UiItems = new ProductModel[2][];
            sectionModel.UiItems[0] = new[]
            {
                new ProductModel
                {
                    ProductType = ProductType.BigLootbox,
                    CurrencyType = CurrencyType.HardCurrency,
                    Cost = 30.ToString(),
                    ImagePreviewPath = "BigLootbox",
                    KitId = "3_1",
                    Name = "BIG BOX",
                    ShopItemSize = ProductSizeEnum.Small,
                }
            }; 
            sectionModel.UiItems[1] = new[]
            {
                new ProductModel
                {
                    ProductType = ProductType.MegaLootbox,
                    CurrencyType = CurrencyType.HardCurrency,
                    Cost = 80.ToString(),
                    ImagePreviewPath = "BigLootbox",
                    KitId = "3_2",
                    Name = "MEGA BOX",
                    ShopItemSize = ProductSizeEnum.Small
                }
            };
            return sectionModel;
        }
    }
}