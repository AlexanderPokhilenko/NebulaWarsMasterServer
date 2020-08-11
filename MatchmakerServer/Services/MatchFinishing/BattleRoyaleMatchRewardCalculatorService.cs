namespace AmoebaGameMatcherServer.Services.MatchFinishing
{
    /// <summary>
    /// По результатам боя в батл рояль режиме присуждает награду игроку.
    /// </summary>
    public class BattleRoyaleMatchRewardCalculatorService
    {
        readonly BattleRoyaleWarshipRatingCalculator warshipRatingCalculator;
        
        public BattleRoyaleMatchRewardCalculatorService()
        {
            warshipRatingCalculator = new BattleRoyaleWarshipRatingCalculator();
        }
        
        public MatchReward Calculate(int placeInMatch, int currentWarshipRating)
        {
            //TODO добавить поддержку double tokens
            //TODO добавить поддержку сундуков
            //TODO решить, чт делать с Json-ом
            
            MatchReward result = new MatchReward
            {
                WarshipRatingDelta = GetWarshipRatingDelta(placeInMatch, currentWarshipRating),
                LootboxPoints = GetPointsForSmallLootbox(placeInMatch, currentWarshipRating)
            };
            return result;
        }
            
        private int GetWarshipRatingDelta(int placeInMatch, int currentWarshipRating)
        {
            int warshipRatingDelta = warshipRatingCalculator.GetWarshipRatingDelta(currentWarshipRating, placeInMatch);
            return warshipRatingDelta;
        }
        
        private int GetPointsForSmallLootbox(int placeInMatch, int currentWarshipRating)
        {
            return 20 - 2 * (placeInMatch-1);
        }
    }
}