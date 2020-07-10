﻿﻿﻿﻿﻿﻿﻿﻿﻿using System.Collections.Generic;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class LootboxModel
    {
        [Index(0)] public virtual List<LootboxPrizeModel> Prizes { get; set; }
    }

    [ZeroFormattable]
    public class LootboxPrizeModel
    {
        [Index(0)] public virtual LootboxPrizeType LootboxPrizeType { get; set; }
        [Index(1)] public virtual int Quantity { get; set; } 
        [Index(2)] public virtual int? WarshipId { get; set; }
    }

    public enum LootboxPrizeType
    {
        SoftCurrency,
        LootboxPoints,
        WarshipPowerPoints
    }
}