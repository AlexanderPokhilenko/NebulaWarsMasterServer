using System.Collections.Generic;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class ShopModel
    {
        [Index(0)] public virtual List<UiContainerModel> UiContainerModel { get; set; }
    }

    [ZeroFormattable]
    public class UiContainerModel
    {
        [Index(0)] public virtual UiItemModel[][] UiItems { get; set; }
    }

    [ZeroFormattable]
    public class UiItemModel
    {
        [Index(0)] public virtual ProductTypeEnum2 ProductType { get; set; }
        [Index(1)] public virtual string ImagePath { get; set; }
        [Index(2)] public virtual decimal CurrentCost { get; set; }
        [Index(3)] public virtual decimal? CostBeforeStock { get; set; }
        [Index(4)] public virtual CurrencyTypeEnum CurrencyType { get; set; }
        [Index(5)] public virtual string ProductGoogleId { get; set; }
        [Index(6)] public virtual string KitId { get; set; }
    }
    
    public enum CurrencyTypeEnum
    {
        RegularCurrency=1,
        PremiumCurrency=2,
        RealCurrency=3
    }

    public enum ProductTypeEnum2
    {
        SmallLootbox,
        BigLootbox,
        MegaLootbox,
        Skin,
        Warship,
        WarshipAndSkin,
        WarshipPowerPoints,
        FreeSmallLootbox,
        PremiumCurrency,
        RegularCurrency
    }
}
