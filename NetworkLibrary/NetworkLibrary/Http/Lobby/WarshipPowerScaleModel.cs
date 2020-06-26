﻿﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    /// <summary>
    /// Информация о шкале силы для кораблей. 
    /// </summary>
    [ZeroFormattable]
    public class WarshipPowerScaleModel
    {
        [Index(0)] public virtual WarshipPowerLevelModel[] PowerLevelModels { get; set; }
    }
}