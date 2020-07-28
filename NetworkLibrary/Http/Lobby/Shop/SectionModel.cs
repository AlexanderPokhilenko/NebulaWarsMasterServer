using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    public enum SectionTypeEnum
    {
        SoftCurrency,
        HardCurrency
    }
    /// <summary>
    /// Описывает раздел в магазине.
    /// </summary>
    [ZeroFormattable]
    public class SectionModel
    {
        //todo заменить на список
        /// <summary>
        /// Таблица товаров
        /// </summary>
        [Index(0)] public virtual ProductModel[][] UiItems { get; set; }
        /// <summary>
        /// Если нужно, чтобы внизу был указатель на этот раздел, то нужно заполнить это свойство.
        /// </summary>
        [Index(1)] public virtual bool NeedFooterPointer { get; set; }
        /// <summary>
        /// Название раздела магазина.
        /// </summary>
        [Index(2)] public virtual string HeaderName { get; set; }
        [Index(3)] public virtual SectionTypeEnum? SectionTypeEnum { get; set; }

        public int ProductsCount()
        {
            return UiItems.SelectMany(item => item).Count();
        }

        public List<ProductModel> GetProducts()
        {
            return UiItems.SelectMany(item => item).ToList();
        } 
        public ProductModel GetProduct(int index)
        {
            return UiItems.SelectMany(item => item).ToList()[index];
        }
    }
}