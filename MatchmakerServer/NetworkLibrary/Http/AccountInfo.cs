﻿using System.Collections.Generic;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class AccountInfo
    {
        [Index(0)] public virtual string Username { get; set; }
        [Index(1)] public virtual int AccountRating { get; set; }
        [Index(2)] public virtual int RegularCurrency { get; set; }
        [Index(3)] public virtual int PremiumCurrency { get; set; }
        [Index(4)] public virtual int PointsForBigChest { get; set; }
        [Index(5)] public virtual int PointsForSmallChest { get; set; }
        
        [Index(6)] public virtual List<WarshipInfo> Warships { get; set; }
    }
}
