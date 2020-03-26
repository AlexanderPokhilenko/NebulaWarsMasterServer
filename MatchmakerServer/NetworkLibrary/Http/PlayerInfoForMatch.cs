﻿﻿﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    /// <summary>
    /// Нужен для 
    /// </summary>
    [ZeroFormattable]
    public class PlayerInfoForMatch
    {
        [Index(0)] public virtual string ServiceId { get; set; }
        [Index(1)] public virtual int TemporaryId { get; set; }
        [Index(2)] public virtual string WarshipName { get; set; }
        [Index(3)] public virtual int WarshipCombatPowerLevel { get; set; }
        [Index(4)] public virtual bool IsBot { get; set; }
        [Index(5)] public virtual int AccountId { get; set; }
        [Index(6)] public virtual int WarshipId { get; set; }
    }
}