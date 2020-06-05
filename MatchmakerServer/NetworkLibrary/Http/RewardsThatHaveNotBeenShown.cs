using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class RewardsThatHaveNotBeenShown
    {
        [Index(0)] public virtual int RegularCurrency {get;set;}
        [Index(1)] public virtual int PremiumCurrency {get;set;}
        [Index(2)] public virtual int PointsForSmallLootbox {get;set;}
        [Index(3)] public virtual int PointsForBigLootbox {get;set;}
        [Index(4)] public virtual int AccountRating {get;set;}
        
        public static RewardsThatHaveNotBeenShown operator +(RewardsThatHaveNotBeenShown arg1,
            RewardsThatHaveNotBeenShown arg2)
        {
            return new RewardsThatHaveNotBeenShown
            {
                RegularCurrency = arg1.RegularCurrency + arg2.RegularCurrency,
                PremiumCurrency = arg1.PremiumCurrency + arg2.PremiumCurrency,
                PointsForSmallLootbox = arg1.PointsForSmallLootbox + arg2.PointsForSmallLootbox,
                PointsForBigLootbox = arg1.PointsForBigLootbox + arg2.PointsForBigLootbox,
                AccountRating = arg1.AccountRating + arg2.AccountRating
            };
        }
    }
}