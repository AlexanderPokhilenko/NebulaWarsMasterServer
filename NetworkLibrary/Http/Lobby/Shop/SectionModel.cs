using JetBrains.Annotations;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    /// <summary>
    /// Описывает раздел в магазине.
    /// </summary>
    [ZeroFormattable]
    public class SectionModel
    {
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
    }
}