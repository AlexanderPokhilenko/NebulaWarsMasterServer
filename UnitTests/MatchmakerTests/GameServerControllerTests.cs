using AmoebaGameMatcherServer.Controllers.Matchmaker;
using AmoebaGameMatcherServer.Experimental;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.MatchFinishing;
using Moq;

namespace MatchmakerTest.MatchmakerTests
{
    [TestClass]
    public class GameServerControllerTests
    {
        [TestMethod]
        [DataRow(null, 0, 0, Globals.GameServerSecret, DisplayName = "AccountId is null")]
        [DataRow(0, null, 0, Globals.GameServerSecret, DisplayName = "PlaceInBattle is null")]
        [DataRow(0, 0, null, Globals.GameServerSecret, DisplayName = "MatchId is null")]
        [DataRow(0, 0, 0, null, DisplayName = "Secret is null")]
        public async Task PlayerDeath_InvalidData_BadRequest(int? accountId, int? placeInBattle, int? matchId, string secret)
        {
            //Arrange
            var controller = new GameServerController(new Mock<IBattleRoyaleMatchFinisherService>().Object);

            //Act
            var result = await controller.PlayerDeath(accountId, placeInBattle, matchId, secret);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        [DataRow(null, Globals.GameServerSecret, DisplayName = "MatchId is null")]
        [DataRow(0, null, DisplayName = "Secret is null")]
        public async Task DeleteMatch_InvalidData_BadRequest(int? matchId, string secret)
        {
            //Arrange
            var controller = new GameServerController(new Mock<IBattleRoyaleMatchFinisherService>().Object);

            //Act
            var result = await controller.DeleteMatch(matchId, secret);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }
    }
}
