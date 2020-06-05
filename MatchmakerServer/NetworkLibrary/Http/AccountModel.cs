using System.Collections.Generic;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class AccountDto
    {
        [Index(0)] public virtual string Username { get; set; }
        [Index(1)] public virtual int AccountRating { get; set; }
        [Index(2)] public virtual int RegularCurrency { get; set; }
        [Index(3)] public virtual int PremiumCurrency { get; set; }
        [Index(4)] public virtual int PointsForBigLootbox { get; set; }
        [Index(5)] public virtual int PointsForSmallLootbox { get; set; }
        [Index(6)] public virtual List<WarshipDto> Warships { get; set; }
    }

}