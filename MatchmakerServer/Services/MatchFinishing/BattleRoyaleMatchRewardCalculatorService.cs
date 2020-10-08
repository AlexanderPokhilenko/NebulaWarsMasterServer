using System;

namespace AmoebaGameMatcherServer.Services.MatchFinishing
{
    public class BattleRoyaleMatchRewardCalculatorService : IBattleRoyaleMatchRewardCalculatorService
    {
        readonly BattleRoyaleWarshipRatingCalculator warshipRatingCalculator;
        
        public BattleRoyaleMatchRewardCalculatorService()
        {
            warshipRatingCalculator = new BattleRoyaleWarshipRatingCalculator();
        }
        
        public MatchReward Calculate(int placeInBattle, int currentWarshipRating)
        {
            //TODO добавить поддержку double tokens
            //TODO добавить поддержку сундуков
            //TODO решить, чт делать с Json-ом
            
            MatchReward result = new MatchReward
            {
                WarshipRatingDelta = GetWarshipRatingDelta(placeInBattle, currentWarshipRating),
                LootboxPoints = GetPointsForSmallLootbox(placeInBattle, currentWarshipRating)
            };
            Console.WriteLine("LootboxPoints = "+result.LootboxPoints);
            return result;
        }
            
        private int GetWarshipRatingDelta(int placeInMatch, int currentWarshipRating)
        {
            int warshipRatingDelta = warshipRatingCalculator.GetWarshipRatingDelta(currentWarshipRating, placeInMatch);
            return warshipRatingDelta;
        }
        
        private int GetPointsForSmallLootbox(int placeInBattle, int currentWarshipRating)
        {
            return 20 - 2 * placeInBattle;
        }
    }
}