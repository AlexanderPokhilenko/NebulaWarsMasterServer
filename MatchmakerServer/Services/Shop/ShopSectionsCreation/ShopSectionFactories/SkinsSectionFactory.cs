using NetworkLibrary.NetworkLibrary.Http;

namespace Code.Scenes.LobbyScene.Scripts
{
    public class SkinsSectionFactory
    {
        public SectionModel Create()
        {
            SectionModel sectionModel = new SectionModel
            {
                NeedFooterPointer = true,
                HeaderName = "SKINS"
            };
            sectionModel.UiItems = new ProductModel[1][];
            
            //первая строка
            sectionModel.UiItems[0] = new[]
            {
                new ProductModel
                {
                    ProductType = ProductType.Skin,
                    CurrencyType = CurrencyType.HardCurrency,
                    Name = "HARE DESTROYER",
                    Cost = 200.ToString(),
                    ShopItemSize = ProductSizeEnum.Big,
                    KitId = "2_1",
                    WarshipModel = new WarshipModel
                    {
                         Description = 
@"Skin includes:
- a special model of a spaceship;
- a special torpedo model;
- special effects.",
                         KitName = "HARE DESTROYER",
                         ViewTypeId = ViewTypeId.HareShip,
                         PrefabPath = "Prefabs/Hare"
                    }
                }, 
                new ProductModel
                {
                    ProductType = ProductType.Skin,
                    CurrencyType = CurrencyType.HardCurrency,
                    Name = "BIRD DESTROYER",
                    Cost = 50.ToString(),
                    ShopItemSize = ProductSizeEnum.Big,
                    KitId = "2_2",
                    WarshipModel = new WarshipModel()
                    {
                        Description = 
                            @"Skin includes:
- a special model of a spaceship;
- a special torpedo model;
- special effects.",
                        KitName = "FLAMING BIRD",
                        ViewTypeId = ViewTypeId.BirdPlayer,
                        PrefabPath = "Prefabs/Bird"
                    }
                }
            };

            return sectionModel;
        }
    }
}