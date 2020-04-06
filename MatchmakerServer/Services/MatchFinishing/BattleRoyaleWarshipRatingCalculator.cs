using System;
using System.Collections.Generic;
using System.Linq;
using AmoebaGameMatcherServer.Utils;

namespace AmoebaGameMatcherServer.Services.MatchFinishing
{
    public class BattleRoyaleWarshipRatingCalculator
    {
        private const int ExpectedLength = Globals.NumbersOfPlayersInBattleRoyaleMatch;
        /// <summary>
        /// Ключ - максимальное значение промежутка.
        /// Значение - массив значений изменений рейтинга корабля в зависимости от места в бою.
        /// </summary>
        private readonly Dictionary<int, int[]> ratingDeltaTable = new Dictionary<int, int[]>
        {
            {49 , new[]{+10,+8,+7,+6,+4,+2,+2,+1,+0,+0}}, 
            {99 , new[]{+10,+8,+7,+6,+3,+2,+2,+0,-1,-2}},
            {199, new[]{+10,+8,+7,+6,+3,+1,+0,-1,-2,-2}}, 
            {299, new[]{+10,+8,+6,+5,+2,+0,+0,-3,-4,-4}}, 
            {399, new[]{+10,+8,+6,+5,+2,-1,-2,-3,-5,-5}}, 
            {499, new[]{+10,+8,+6,+4,+2,-1,-2,-5,-6,-6}}, 
            {599, new[]{+10,+8,+6,+4,+1,-2,-2,-5,-7,-8}}, 
            {699, new[]{+10,+8,+6,+4,+1,-3,-4,-5,-8,-9}} 
        };

        public BattleRoyaleWarshipRatingCalculator()
        {
            CheckRatingTable();
        }
        
        /// <summary>
        /// Проверяет, что в таблице в каждой строке есть ровно столько занчений, сколько мест в бою.
        /// </summary>
        private void CheckRatingTable()
        {
            foreach (var pair in ratingDeltaTable)
            {
                if (pair.Value.Length != ExpectedLength)
                {
                    string errMessage = $"Размер таблицы для вычисления рейтинга неверный." +
                                        $" {nameof(ExpectedLength)} {ExpectedLength} " +
                                        $"{nameof(pair.Value.Length)} {pair.Value.Length}" +
                                        $"{nameof(pair.Key)} {pair.Key}";
                    throw new Exception(errMessage);
                }
            }
        }
        
        public int GetWarshipRatingDelta(int currentWarshipRating, int placeInMatch)
        {
            if (placeInMatch < 1 || placeInMatch > ExpectedLength)
            {
                throw new ArgumentOutOfRangeException(nameof(placeInMatch));
            }
            if (currentWarshipRating < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(currentWarshipRating));   
            }
            
            int maxRangeRating = ratingDeltaTable.Keys
                .FirstOrDefault(maxRangeRating1=> currentWarshipRating <= maxRangeRating1);
            
            //Если значение рейтинга корабля больше максимального в таблице, то пусть рейтинг вычисляется по самому 
            //жёсткому правилу.
            if (maxRangeRating == default)
            {
                maxRangeRating = ratingDeltaTable.Keys.Last();
            }
            
            return ratingDeltaTable[maxRangeRating][placeInMatch-1];
        }
    }
}