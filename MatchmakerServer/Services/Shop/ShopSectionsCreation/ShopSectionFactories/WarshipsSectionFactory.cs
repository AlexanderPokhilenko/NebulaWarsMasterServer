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
                    ProductType = ProductType.Warship,
                    CurrencyType = CurrencyType.HardCurrency,
                    Name = "HARE",
                    Cost = 200.ToString(),
                    ShopItemSize = ProductSizeEnum.Big,
                    KitId = "15_1",
                    WarshipModel = new WarshipModel
                    {
                        Description = 
@"The hare attacks the enemies with four cannons. It is great for suppression fire. His ability is a shot with a huge charge of plasma.",
                        KitName = "HARE DESTROYER",
                        ViewTypeId = ViewTypeId.HareShip,
                        PrefabPath = "Prefabs/Hare"
                    }
                }
            };

            return sectionModel;
        }
    }
}