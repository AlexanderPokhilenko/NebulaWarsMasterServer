using System.Linq;
using System.Threading.Tasks;
using DataLayer.Tables;
using LibraryForTests;
using NUnit.Framework;

namespace IntegrationTests
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
            var warshipPowerPoints = originalAccount.GetWarshipPowerPoints(warshipId);
            var warshipPowerLevel = originalAccount.GetWarshipPowerLevel(warshipId);

            //Act
            WarshipImprovementCostChecker.CanAPurchaseBeMade(originalAccountSoftCurrency, warshipPowerPoints,
                warshipPowerLevel, out var faultReason);
            bool success = await WarshipImprovementFacadeService.TryBuyLevel(originalAccount.ServiceId, warshipId);

            //Assert
            Assert.IsTrue(success);
            //todo кусок говна
        }
    }
}