namespace AmoebaGameMatcherServer.Services.MatchFinishing
{
    public class MatchReward
    {
        public int WarshipRatingDelta { get; set; }
        public int PremiumCurrencyDelta { get; set; }
        public int SoftCurrencyDelta { get; set; }
        public object JsonMatchResultDetails { get; set; }
        public int BigLootboxPoints { get; set; }
        public int SmallLootboxPoints { get; set; }
    }
}