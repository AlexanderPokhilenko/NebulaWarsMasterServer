using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class RewardsThatHaveNotBeenShown
    {
        [Index(0)] public virtual int SoftCurrency {get;set;}
        [Index(1)] public virtual int HardCurrency {get;set;}
        [Index(2)] public virtual int SmallLootboxPoints {get;set;}
        [Index(3)] public virtual int AccountRating {get;set;}

        public override string ToString()
        {
            return
                $"{nameof(SoftCurrency)} {SoftCurrency} " +
                $"{nameof(SmallLootboxPoints)} {SmallLootboxPoints} " +
                $"{nameof(AccountRating)} {AccountRating}";
        }

        public static RewardsThatHaveNotBeenShown operator +(RewardsThatHaveNotBeenShown arg1,
            RewardsThatHaveNotBeenShown arg2)
        {
            return new RewardsThatHaveNotBeenShown
            {
                SoftCurrency = arg1.SoftCurrency + arg2.SoftCurrency,
                SmallLootboxPoints = arg1.SmallLootboxPoints + arg2.SmallLootboxPoints,
                AccountRating = arg1.AccountRating + arg2.AccountRating,
                HardCurrency = arg1.HardCurrency+arg2.HardCurrency
            };
        }
    }
}