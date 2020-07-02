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
            AccountBuilder accountBuilder = new AccountBuilder(2);
            AccountDirector accountDirector = new SmallAccountDirector(accountBuilder, Context);
            accountDirector.WriteToDatabase();
            Account originalAccount = accountDirector.GetAccount();
            int originalAccountSoftCurrency = accountDirector.GetAccountSoftCurrency();
            int warshipId = originalAccount.Warships.First().Id;
            var warshipPowerPoints = accountDirector.GetWarshipPowerPoints(warshipId);
            var warshipPowerLevel = accountDirector.GetWarshipPowerLevel(warshipId);

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