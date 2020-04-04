using DataLayer.Tables;

namespace AmoebaGameMatcherServer.Services
{
    public class BattleRoyaleMatchRewardService
    {
        readonly BattleRoyaleWarshipRatingCalculator warshipRatingCalculator;
        public BattleRoyaleMatchRewardService()
        {
            warshipRatingCalculator = new BattleRoyaleWarshipRatingCalculator();
        }
        public MatchReward GetMatchReward(int placeInMatch, int currentWarshipRating)
        {
            //TODO добавить поддержку double tokens
            //TODO добавить поддержку сундуков
            //TODO решить, чт делать с Json-ом
            
            MatchReward result = new MatchReward
            {
                WarshipRatingDelta = GetWarshipRatingDelta(placeInMatch, currentWarshipRating),
                PremiumCurrencyDelta = 0,
                RegularCurrencyDelta = GetRegularCurrencyDelta(placeInMatch, currentWarshipRating),
                JsonMatchResultDetails = null,
                PointsForBigChest = 0,
                PointsForSmallChest = 0
            };
            return result;
        }

        private int GetRegularCurrencyDelta(int placeInMatch, int currentWarshipRating)
        {
            if (placeInMatch < 5)
            {
                return 10;
            }
            else
            {
                return 20;
            }
        }
        
        private int GetWarshipRatingDelta(int placeInMatch, int currentWarshipRating)
        {
            int warshipRatingDelta = warshipRatingCalculator.GetWarshipRatingDelta(currentWarshipRating, placeInMatch);
            return warshipRatingDelta;
        }
    }
}