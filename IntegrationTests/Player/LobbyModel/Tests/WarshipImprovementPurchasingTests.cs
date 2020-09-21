using System;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Tables;
using IntegrationTests.Player.LobbyModel.Config;
using LibraryForTests;
using NUnit.Framework;

namespace IntegrationTests.Player.LobbyModel.Tests
{
    [TestFixture]
    internal sealed class WarshipImprovementPurchasingTests : BaseIntegrationFixture
    {
        [Test]
        public async Task SmallAccount()
        {
            //Arrange
            string serviceId = "serviceId";
            Account originalAccount = await DefaultAccountFactoryService.CreateDefaultAccountAsync(serviceId);
            int originalAccountSoftCurrency = originalAccount.GetAccountSoftCurrency();
            int warshipId = originalAccount.Warships.First().Id;
            int warshipPowerPoints = originalAccount.GetWarshipPowerPoints(warshipId);
            int warshipPowerLevel = originalAccount.GetWarshipPowerLevel(warshipId);

            //Act
            bool canAPurchaseBeMade = WarshipImprovementCostChecker
                .CanAPurchaseBeMade(originalAccountSoftCurrency, warshipPowerPoints,
                    warshipPowerLevel, out var faultReason);

            Console.WriteLine($"{nameof(canAPurchaseBeMade)} = {canAPurchaseBeMade}");
            bool success = await WarshipImprovementFacadeService.TryBuyLevel(originalAccount.ServiceId, warshipId);

            //Assert
            Assert.IsTrue(success);
        }
    }
}