using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace Code.Scenes.LobbyScene.Scripts
{
    public class WarshipsSectionFactory
    {
        public SectionModel Create()
        {
            SectionModel sectionModel = new SectionModel
            {
                NeedFooterPointer = false,
                HeaderName = "WARSHIPS"
            };
            sectionModel.UiItems = new ProductModel[1][];
            
            //первая строка
            sectionModel.UiItems[0] = new[]
            {
                new ProductModel
                {
                    TransactionType = TransactionTypeEnum.Warship,
                    CurrencyTypeEnum = CurrencyTypeEnum.HardCurrency,
                    Name = "HARE",
                    CostString = 200.ToString(),
                    Cost = 200,
                    ShopItemSize = ProductSizeEnum.Big,
                    Id = 18,
                    WarshipModel = new WarshipModel
                    {
                        Description = 
@"The hare attacks the enemies with four cannons. It is great for suppression fire. His ability is a shot with a huge charge of plasma.",
                        KitName = "HARE DESTROYER",
                        PrefabPath = "Prefabs/Hare"
                    },
                    ImagePreviewPath = "hare"
                }
            };

            return sectionModel;
        }
    }
}