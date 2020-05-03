using System.Collections.Generic;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class LootboxData
    {
        [Index(0)] public virtual List<LootboxPrizeData> Prizes { get; set; }
    }

    [ZeroFormattable]
    public class LootboxPrizeData
    {
        [Index(0)] public virtual LootboxPrizeType LootboxPrizeType { get; set; }
        [Index(1)] public virtual int Quantity { get; set; } 
    }

    public enum LootboxPrizeType
    {
        RegularCurrency,
        PointsForSmallLootbox
    }
}