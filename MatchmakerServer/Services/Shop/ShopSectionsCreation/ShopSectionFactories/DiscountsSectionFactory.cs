using System;
using NetworkLibrary.NetworkLibrary.Http;

namespace Code.Scenes.LobbyScene.Scripts
{
    public class DiscountsSectionFactory
    {
        public SectionModel Create()
        {
            SectionModel sectionModel = new SectionModel
            {
                NeedFooterPointer = true,
                HeaderName = "SPECIAL OFFER"
            };
            sectionModel.UiItems = new ProductModel[1][];
            
            //первая строка
            sectionModel.UiItems[0] = new[]
            {
                new ProductModel
                {
                    KitId = "pokaszhiMneDengi654654",
                    ProductType = ProductType.MegaLootbox,
                    CurrencyType = CurrencyType.HardCurrency,
                    ImagePreviewPath = "BigLootbox",
                    Name = "MEGA BOX",
                    Cost = 139.ToString(),
                    ShopItemSize = ProductSizeEnum.Big,
                    DiscountPrice = new DiscountPrice
                    {
                        CostBeforeDiscount = 320
                    },
                    UtcDeadline = DateTime.UtcNow+TimeSpan.FromHours(4)
                }
            };

            return sectionModel;
        }
    }
}