﻿﻿﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class PlayerInfoForGameRoom
    {
        [Index(0)] public virtual string GoogleId { get; set; }
        [Index(1)] public virtual int TemporaryId { get; set; }
        [Index(2)] public virtual string WarshipName { get; set; }
        [Index(3)] public virtual int WarshipCombatPowerLevel { get; set; }
        [Index(4)] public virtual bool IsBot { get; set; }
    }
}