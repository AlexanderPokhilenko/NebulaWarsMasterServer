namespace DataLayer.Tables
{
    public struct MatchReward
    {
        public int WarshipRatingDelta { get; set; }
        public int RegularCurrencyDelta { get; set; }
        public int PremiumCurrencyDelta { get; set; }
        //TODO придумать нормальное название
        public int PointsForBigChest { get; set; }
        //TODO придумать нормальное название
        public int PointsForSmallLootbox { get; set; }
        public string JsonMatchResultDetails { get; set; }
    }
}