using DataLayer.Tables;
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
                    TransactionType = TransactionTypeEnum.Lootbox,
                    CurrencyType = CurrencyType.HardCurrency,
                    CostString = 30.ToString(),
                    Cost = 30,
                    ImagePreviewPath = "BigLootbox",
                    Id = 10,
                    Name = "BIG BOX",
                    ShopItemSize = ProductSizeEnum.Small,
                }
            }; 
            sectionModel.UiItems[1] = new[]
            {
                new ProductModel
                {
                    TransactionType = TransactionTypeEnum.Lootbox,
                    CurrencyType = CurrencyType.HardCurrency,
                    CostString = 80.ToString(),
                    Cost = 80,
                    ImagePreviewPath = "BigLootbox",
                    Id = 11,
                    Name = "MEGA BOX",
                    ShopItemSize = ProductSizeEnum.Small
                }
            };
            return sectionModel;
        }
    }
}