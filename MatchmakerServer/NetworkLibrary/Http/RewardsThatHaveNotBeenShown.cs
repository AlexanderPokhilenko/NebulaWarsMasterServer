using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class RewardsThatHaveNotBeenShown
    {
        [Index(0)] public virtual int RegularCurrency {get;set;}
        [Index(1)] public virtual int PremiumCurrency {get;set;}
        [Index(2)] public virtual int PointsForSmallChest {get;set;}
        [Index(3)] public virtual int PointsForBigChest {get;set;}
        [Index(4)] public virtual int Rating {get;set;}
    }
}