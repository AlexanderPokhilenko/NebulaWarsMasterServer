﻿using System;
 using System.Collections.Generic;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class ShopModel
    {
        [Index(0)] public virtual List<ShopSectionModel> UiContainerModel { get; set; }
    }

    [ZeroFormattable]
    public class ShopSectionModel
    {
        [Index(0)] public virtual ShopItemModel[][] UiItems { get; set; }
        [Index(1)] public virtual ShopSectionTypeEnum? ShopSectionTypeEnum { get; set; }
        [Index(2)] public virtual string StubHeaderName { get; set; }
    }

    public enum ShopSectionTypeEnum
    {
        Discounts=1,
        DailyDeals=2,
        Skins=3,
        Boxes=4,
        PremiumCurrency=5,
        RegularCurrency=6
    }
    
    [ZeroFormattable]
    public class ShopItemModel
    {
        [Index(0)] public virtual ProductTypeEnum2 ProductType { get; set; }
        [Index(1)] public virtual string ImagePath { get; set; }
        [Index(2)] public virtual decimal Cost { get; set; }
        [Index(3)] public virtual CurrencyTypeEnum CurrencyType { get; set; }
        [Index(4)] public virtual string KitId { get; set; }
        [Index(5)] public virtual string Name { get; set; }
        /// <summary>
        /// Пометка для красоты.
        /// </summary>
        [Index(6)] public virtual ItemMark ItemMark { get; set; }
        /// <summary>
        /// Если товар покупается за реальнаые деньги в Google или Apple
        /// </summary>
        [Index(7)] public virtual ForeignServiceProduct ForeignServiceProduct { get; set; }
        /// <summary>
        /// Если на товар действует скидка
        /// </summary>
        [Index(8)] public virtual DiscountPrice DiscountPrice { get; set; }
        /// <summary>
        /// Если товар является усилением корабля
        /// </summary>
        [Index(9)] public virtual WarshipPowerPointsProduct WarshipPowerPointsProduct { get; set; }
        /// <summary>
        /// Вертикальный размер товара в секции
        /// </summary>
        [Index(10)] public virtual ShopItemSizeEnum? ShopItemSize { get; set; }
        /// <summary>
        /// Если товар имеет срок годности. Например, ежедневные акции.
        /// </summary>
        [Index(11)] public virtual DateTime? UtcDeadline { get; set; }
        
        /// <summary>
        /// Если по акции продаётся 5 мега сундуков, то модификатор будет 5
        /// </summary>
        [Index(12)] public virtual int? MagnificationRatio { get; set; }
    }
    
    public enum ShopItemSizeEnum
    {
        Small,
        Big
    }
    
    [ZeroFormattable]
    public class ItemMark
    {
        [Index(0)] public virtual MarkType MarkType { get; set; }
        [Index(1)] public virtual int? NumberOfPercent { get; set; }
    }
    
    public enum MarkType
    {
        Popularly,
        MostProfitable,
        MoreProfitableInPercents
    }
    
    [ZeroFormattable]
    public class ForeignServiceProduct
    {
        [Index(0)] public virtual string ProductGoogleId { get; set; }
    }
    
    [ZeroFormattable]
    public class DiscountPrice
    {
        [Index(0)] public virtual decimal? CostBeforeDiscount { get; set; }
    }
    
    [ZeroFormattable]
    public class WarshipPowerPointsProduct
    {
        [Index(0)] public virtual int WarshipId { get; set; }
        [Index(1)] public virtual int PowerPointsAmount{ get; set; }
    }
    
    public enum CurrencyTypeEnum
    {
        RegularCurrency = 1,
        PremiumCurrency = 2,
        RealCurrency = 3,
        Free = 4
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
        PremiumCurrency,
        RegularCurrency,
        DailyPresent
    }
}
