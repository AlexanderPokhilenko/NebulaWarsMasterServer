using System;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    public enum ProductMarkTypeEnum
    {
        New,
        Popular,
        Sale,
        Timer
    }

    [ZeroFormattable]
    public class TimerProductMarkModel
    {
        [Index(0)] public virtual DateTime UtcDeadline { get; set; }
    }
    
    [ZeroFormattable]
    public class SaleProductMarkModel
    {
        [Index(0)] public virtual int PercentageDiscount { get; set; }
    }
    
    /// <summary>
    /// Хранит информацию про пометки на продукте.
    /// </summary>
    [ZeroFormattable]
    public class ProductMark
    {
        [Index(0)] public virtual ProductMarkTypeEnum ProductMarkTypeEnum { get; set; }
        [Index(1)] public virtual byte[] SerializedProductMark { get; set; }
    }
    

    [ZeroFormattable]
    public class InGameCurrencyCostModel
    {
        [Index(0)] public virtual decimal Cost { get; set; }
    }
    
    [ZeroFormattable]
    public class RealCurrencyCostModel
    {
        [Index(0)] public virtual bool IsConsumable { get; set; }
        [Index(1)] public virtual string GoogleProductId { get; set; }
        [Index(2)] public virtual string AppleProductId { get; set; }
        [Index(3)] public virtual string CostString { get; set; }
    }
    
    [ZeroFormattable]
    public class CostModel
    {
        [Index(0)] public virtual CostTypeEnum CostTypeEnum { get; set; }
        [Index(1)] public virtual byte[] SerializedCostModel { get; set; }
    }

    [ZeroFormattable]
    public class HardCurrencyProductModel
    {
        [Index(0)] public virtual int Amount { get; set; }
    }
    [ZeroFormattable]
    public class SoftCurrencyProductModel
    {
        [Index(0)] public virtual int Amount { get; set; }
    }
    
    [ZeroFormattable]
    public class LootboxPointsProductModel
    {
        [Index(0)] public virtual int AmountOfLootboxPoints { get; set; }
    } 
    
    [ZeroFormattable]
    public class WarshipPowerPointsProductModel
    {
        [Index(0)] public virtual int Increment { get; set; }
        [Index(1)] public virtual int WarshipId { get; set; }
        [Index(2)] public virtual WarshipTypeEnum WarshipTypeEnum { get; set; }
        [Index(3)] public virtual WppSupportClientModel SupportClientModel { get; set; }
    }

    [ZeroFormattable]
    public class WppSupportClientModel
    {
        [Index(0)] public virtual string WarshipSkinName { get; set; }
        [Index(1)] public virtual int MaxValueForLevel { get; set; }
        [Index(2)] public virtual int StartValue { get; set; }
    }
    
    /// <summary>
    /// Описывает товар в разделе.
    /// </summary>
    [ZeroFormattable]
    public class ProductModel
    {
        [Index(0)] public virtual int Id { get; set; }
        [Index(1)] public virtual ResourceTypeEnum ResourceTypeEnum { get; set; }
        [Index(2)] public virtual byte[] SerializedModel { get; set; }
        [Index(3)] public virtual CostModel CostModel { get; set; }
        [Index(4)] public virtual ProductSizeEnum ProductSizeEnum { get; set; }
        [Index(5)] public virtual bool IsDisabled { get; set; }
        [Index(6)] public virtual ProductMark ProductMark { get; set; }
        [Index(7)] public virtual string PreviewImagePath { get; set; }
        
        public static implicit operator HardCurrencyProductModel(ProductModel productModel)
        {
            var model = ZeroFormatterSerializer.Deserialize<HardCurrencyProductModel>(productModel.SerializedModel);
            return model;
        } 
        
        public static implicit operator SoftCurrencyProductModel(ProductModel productModel)
        {
            var model = ZeroFormatterSerializer.Deserialize<SoftCurrencyProductModel>(productModel.SerializedModel);
            return model;
        } 
        
        public static implicit operator LootboxPointsProductModel(ProductModel productModel)
        {
            var model = ZeroFormatterSerializer.Deserialize<LootboxPointsProductModel>(productModel.SerializedModel);
            return model;
        }
        
        public static implicit operator WarshipPowerPointsProductModel(ProductModel productModel)
        {
            var model =
                ZeroFormatterSerializer.Deserialize<WarshipPowerPointsProductModel>(productModel.SerializedModel);
            return model;
        }
    }
}