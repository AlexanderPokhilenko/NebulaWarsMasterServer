﻿﻿﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    /// <summary>
    /// Нужен для гейм сервера. Хранит всю информацию, которая влияет на бой.
    /// </summary>
    [ZeroFormattable]
    public class PlayerInfoForMatch
    {
        /// <summary>
        /// Username игрока
        /// </summary>
        [Index(0)] public virtual string ServiceId { get; set; }
        
        //TODO какое-то говнище
        [Index(1)] public virtual int TemporaryId { get; set; }
        
        /// <summary>
        /// Показатель силы корабля
        /// </summary>
        [Index(2)] public virtual string PrefabName { get; set; }
        
        /// <summary>
        /// Прокачка корабля
        /// </summary>
        [Index(3)] public virtual int WarshipCombatPowerLevel { get; set; }
        
        //TODO какое-то говнище
        [Index(4)] public virtual int AccountId { get; set; }
    }
}