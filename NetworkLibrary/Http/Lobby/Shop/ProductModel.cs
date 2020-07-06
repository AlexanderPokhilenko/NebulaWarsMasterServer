using System;
using DataLayer.Tables;
using JetBrains.Annotations;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    /// <summary>
    /// Описывает товар в разделе.
    /// </summary>
    [ZeroFormattable]
    public class ProductModel
    {
        [Index(0)] public virtual TransactionTypeEnum TransactionType { get; set; }
        /// <summary>
        /// Путь к картинке, которую нужно использовать в разделе магазина.
        /// </summary>
        [Index(1)] public virtual string ImagePreviewPath { get; set; }
        /// <summary>
        /// Стоимость может быть представлена числом для покупок за внутриигровую валюту
        /// или числом + типом валюты за настоящую валюту.
        /// </summary>
        [Index(2)] public virtual string CostString { get; set; }
        [Index(3)] public virtual CurrencyType CurrencyType { get; set; }
       
        [Index(4)] public virtual int Id { get; set; }
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
        [Index(14)] public virtual int Cost { get; set; }
        [Index(15)] public virtual bool Disabled { get; set; }
        [Index(16)] public virtual string SkinPrefabPath { get; set; }
        [Index(17)] public virtual int Amount { get; set; }
    }
}