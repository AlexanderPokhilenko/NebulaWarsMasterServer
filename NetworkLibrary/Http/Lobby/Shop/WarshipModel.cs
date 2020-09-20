using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class WarshipModel
    {
        [Index(0)] public virtual string Description{ get; set; }
        /// <summary>
        /// Скин или корабль или кораль плюс скин
        /// </summary>
        [Index(1)] public virtual string KitName { get; set; }
        /// <summary>
        /// Нужен для того, чтобы заспавнить корабль при просмотре.
        /// </summary>
        [Index(2)] public virtual string PrefabPath { get; set; }
        [Index(3)]public virtual int WarshipId { get; set; }
    }
}