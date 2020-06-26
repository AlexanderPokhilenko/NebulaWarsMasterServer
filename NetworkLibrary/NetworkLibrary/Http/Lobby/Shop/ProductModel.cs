using System;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    /// <summary>
    /// Описывает товар в резаделе.
    /// </summary>
    [ZeroFormattable]
    public class ProductModel
    {
        [Index(0)] public virtual ProductType ProductType { get; set; }
        /// <summary>
        /// Путь к картинке, которую нужно использовать в разделе магазина.
        /// </summary>
        [Index(1)] public virtual string ImagePreviewPath { get; set; }
        /// <summary>
        /// Стоимость может быть представлена числом для покупок за внутриигровую валюту
        /// или числом + типом валюты за настоящую валюту.
        /// </summary>
        [Index(2)] public virtual string Cost { get; set; }
        [Index(3)] public virtual CurrencyType CurrencyType { get; set; }
        /// <summary>
        /// Id типа товара. В один товар может быть включено несколько других.
        /// Например, набор коробок в n штук или корабль + скин
        /// </summary>
        [Index(4)] public virtual string KitId { get; set; }
        [Index(5)] public virtual string Name { get; set; }
        /// <summary>
        /// Пометка для красоты. Например, "Новое", "Акция", "Популярно"
        /// </summary>
        [Index(6)] public virtual ProductMark ProductMark { get; set; }
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
        [Index(10)] public virtual ProductSizeEnum? ShopItemSize { get; set; }
        /// <summary>
        /// Если товар имеет срок годности. Например, ежедневные акции.
        /// </summary>
        [Index(11)] public virtual DateTime? UtcDeadline { get; set; }
        
        /// <summary>
        /// Если по акции продаётся 5 мега сундуков, то модификатор будет 5
        /// </summary>
        [Index(12)] public virtual int? MagnificationRatio { get; set; }
        
        [Index(13)] public virtual WarshipModel WarshipModel { get; set; }
    }

    [ZeroFormattable]
    public class WarshipModel
    {
        [Index(0)] public virtual string PrefabPath { get; set; }
        [Index(1)] public virtual string Description{ get; set; }
        /// <summary>
        /// Скин или корабль или кораль плюс скин
        /// </summary>
        [Index(2)] public virtual string KitName { get; set; }
    }
}