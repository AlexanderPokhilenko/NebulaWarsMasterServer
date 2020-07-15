using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.ShopModel.DeleteMeShopSectionFactories
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
                    TransactionType = TransactionTypeEnum.Skin,
                    CurrencyTypeEnum = CurrencyTypeEnum.HardCurrency,
                    Name = "HARE DESTROYER",
                    CostString = 200.ToString(),
                    Cost = 200,
                    ShopItemSize = ProductSizeEnum.Big,
                    Id = 12,
                    WarshipModel = new WarshipModel
                    {
                         Description = 
@"Skin includes:
- a special model of a spaceship;
- a special torpedo model;
- special effects.",
                         KitName = "HARE DESTROYER",
                         PrefabPath = "Prefabs/Hare"
                    },
                    ImagePreviewPath = "hare"
                }, 
                new ProductModel
                {
                    TransactionType = TransactionTypeEnum.Skin,
                    CurrencyTypeEnum = CurrencyTypeEnum.HardCurrency,
                    Name = "BIRD DESTROYER",
                    CostString = 50.ToString(),
                    Cost = 50,
                    ShopItemSize = ProductSizeEnum.Big,
                    Id = 13,
                    WarshipModel = new WarshipModel()
                    {
                        Description = 
                            @"Skin includes:
- a special model of a spaceship;
- a special torpedo model;
- special effects.",
                        KitName = "FLAMING BIRD",
                        PrefabPath = "Prefabs/Bird"
                    },
                    ImagePreviewPath = "bird"
                }
            };

            return sectionModel;
        }
    }
}