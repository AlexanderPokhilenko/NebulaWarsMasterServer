﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class WarshipInfo
    {
        [Index(0)] public virtual string PrefabName { get; set; }
        [Index(1)] public virtual int CombatPowerLevel { get; set; }
        [Index(2)] public virtual int CombatPowerValue { get; set; }
        [Index(3)] public virtual int Rating { get; set; }
        [Index(4)] public virtual int Id { get; set; }
    }
}