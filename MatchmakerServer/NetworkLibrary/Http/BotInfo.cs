using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class BotInfo
    {
        [Index(0)] public virtual string BotName { get; set; }
        //TODO какое-то говнище
        [Index(1)] public virtual int TemporaryId { get; set; }
        /// <summary>
        /// Название корабля
        /// </summary>
        [Index(2)] public virtual string PrefabName { get; set; }
        /// <summary>
        /// Показатель силы корабля
        /// </summary>
        [Index(3)] public virtual int WarshipCombatPowerLevel { get; set; }
    }
}