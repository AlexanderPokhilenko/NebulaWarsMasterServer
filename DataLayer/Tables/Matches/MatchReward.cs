namespace DataLayer.Tables
{
    public struct MatchReward
    {
        public int WarshipRatingDelta { get; set; }
        public int RegularCurrencyDelta { get; set; }
        public int PremiumCurrencyDelta { get; set; }
        public int PointsForBigChest { get; set; }
        public int PointsForSmallChest { get; set; }
        public string JsonMatchResultDetails { get; set; }
    }
}