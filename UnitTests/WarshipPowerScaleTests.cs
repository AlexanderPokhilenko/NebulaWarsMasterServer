using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkLibrary.Http.Lobby;
using NetworkLibrary.NetworkLibrary.Http;

namespace MatchmakerTest
{
    [TestClass]
    public class WarshipPowerScaleTests
    {
        [TestMethod]
        [DataRow(1,  2   * 10, 4    * 5)]
        [DataRow(2,  3   * 10, 7    * 5)]
        [DataRow(3,  5   * 10, 15   * 5)]
        [DataRow(4,  8   * 10, 26   * 5)]
        [DataRow(5,  13  * 10, 48   * 5)]
        [DataRow(6,  21  * 10, 89   * 5)]
        [DataRow(7,  34  * 10, 163  * 5)]
        [DataRow(8,  55  * 10, 300  * 5)]
        [DataRow(9,  89  * 10, 552  * 5)]
        [DataRow(10, 144 * 10, 1015 * 5)]
        public void GetModel_ValidLevel_OkResult(int powerLevel, int expectedPowerPoints, int expectedSoftCurrency)
        {
            //Arrange
            WarshipImprovementModel model;

            //Act
            model = WarshipPowerScale.GetModel(powerLevel);

            //Assert
            Assert.AreEqual(expectedPowerPoints, model.PowerPointsCost, "Wrong power points.");
            Assert.AreEqual(expectedSoftCurrency, model.SoftCurrencyCost, "Wrong soft currency.");
        }

        [TestMethod]
        [Timeout(25)] //Assert
        public void GetModel_ValidLevel_FastRecalculations()
        {
            for (var i = 1; i <= 10; i++)
            {
                WarshipPowerScale.GetModel(i);
            }
        }
    }
}
