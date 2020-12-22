using AmoebaGameMatcherServer.Services.MatchFinishing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MatchmakerTest.MatchFinishing
{
    [TestClass]
    public class BattleRoyaleWarshipRatingCalculatorTests
    {
        /// <summary>
        /// Нормальные входные данные. Номальный ответ.
        /// </summary>
        [TestMethod]
        public void GetWarshipRatingDelta_NormalData_OK()
        {
            //Arrange
            BattleRoyaleWarshipRatingCalculator ratingCalculator = new BattleRoyaleWarshipRatingCalculator();
            //Act
            int warshipRatingDelta = ratingCalculator.GetWarshipRatingDelta(0, 1);
            //Assert
            Assert.AreEqual(10, warshipRatingDelta);
        }
        
        /// <summary>
        /// Отрицательный рейтинг корабля. Иcключение
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetWarshipRatingDelta_NegativeRating_ArgumentOutOfRangeException()
        {
            //Arrange
            BattleRoyaleWarshipRatingCalculator ratingCalculator = new BattleRoyaleWarshipRatingCalculator();
            //Act
            int warshipRatingDelta = ratingCalculator.GetWarshipRatingDelta(-1, 5);
            //Assert
            Assert.Fail();
        }
        
        /// <summary>
        /// Место в матче меньше 1 или больше 10. Иcключение
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(11)]
        [DataRow(10001)]
        public void GetWarshipRatingDelta_InvalidPlaceInMatch_ArgumentOutOfRangeException(int placeInMatch)
        {
            //Arrange
            BattleRoyaleWarshipRatingCalculator ratingCalculator = new BattleRoyaleWarshipRatingCalculator();
            //Act
            int warshipRatingDelta = ratingCalculator.GetWarshipRatingDelta(1, placeInMatch);
            //Assert
            Assert.Fail();
        }
        
        /// <summary>
        /// Рейтинг корабля больше максимального в таблице. Вернёт ответ по последнему интервалу.
        /// </summary>
        [TestMethod]
        [DataRow(700)]
        [DataRow(1000)]
        public void GetWarshipRatingDelta_BigRating_MaxDelta10(int warshipRating)
        {
            //Arrange
            BattleRoyaleWarshipRatingCalculator ratingCalculator = new BattleRoyaleWarshipRatingCalculator();
            //Act
            int warshipRatingDelta = ratingCalculator.GetWarshipRatingDelta(warshipRating, 1);
            //Assert
            Assert.AreEqual(10, warshipRatingDelta);
        }
        
    }
}