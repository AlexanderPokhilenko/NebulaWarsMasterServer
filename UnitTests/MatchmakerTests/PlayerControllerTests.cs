using AmoebaGameMatcherServer.Controllers.Matchmaker;
using AmoebaGameMatcherServer.Experimental;
using AmoebaGameMatcherServer.Services.PlayerQueueing;
using AmoebaGameMatcherServer.Services.Queues;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NetworkLibrary.NetworkLibrary.Http;
using System.Threading.Tasks;

namespace MatchmakerTest.MatchmakerTests
{
    [TestClass]
    public class PlayerControllerTests
    {
        [TestMethod]
        public void DeleteFromQueue_PlayerIdIsNull_BadRequest()
        {
            //Arrange
            var controller = new PlayerController(new Mock<IBattleRoyaleQueueSingletonService>().Object,
                new Mock<IMatchmakerFacadeService>().Object);

            //Act
            var result = controller.DeleteFromQueue(null);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void DeleteFromQueue_PlayerIdIsNormal_OkResult()
        {
            //Arrange
            var queueSingletonStub = new Mock<IBattleRoyaleQueueSingletonService>();
            queueSingletonStub.Setup(s => s.TryRemove(It.IsNotNull<string>())).Returns(true);

            var controller = new PlayerController(queueSingletonStub.Object,
                new Mock<IMatchmakerFacadeService>().Object);

            //Act
            var result = controller.DeleteFromQueue("NotNullPlayerId");

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkResult));
        }

        [TestMethod]
        public async Task GetMatchData_PlayerIdIsNull_BadRequest()
        {
            //Arrange
            var controller = new PlayerController(new Mock<IBattleRoyaleQueueSingletonService>().Object,
                new Mock<IMatchmakerFacadeService>().Object);

            //Act
            var result = await controller.GetMatchData(null, 0);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task GetMatchData_PlayerIdIsNormal_OkResult()
        {
            //Arrange
            var stubResponse = new MatchmakerResponse();
            var matchmakerFacadeStub = new Mock<IMatchmakerFacadeService>();
            matchmakerFacadeStub.Setup(s => s.GetMatchDataAsync(It.IsNotNull<string>(), It.IsAny<int>())).ReturnsAsync(stubResponse);

            var controller = new PlayerController(new Mock<IBattleRoyaleQueueSingletonService>().Object,
                matchmakerFacadeStub.Object);

            //Act
            var result = await controller.GetMatchData("NotNullPlayerId", 0);

            //Assert
            Assert.AreEqual(stubResponse.SerializeToBase64String(), result.Value);
        }
    }
}
