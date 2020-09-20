using System.Linq;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services;
using DataLayer.Tables;
using LibraryForTests;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    internal sealed class AccountDbReaderTests : BaseIntegrationFixture
    {
        [Test]
        public async Task SmallAccount()
        {
            //Arrange
            string serviceId = "serviceId";
            Account originalAccount = await DefaultAccountFactoryService.CreateDefaultAccountAsync(serviceId);
            int originalAccountRating = originalAccount.GetAccountRating();
            int originalAccountSoftCurrency = originalAccount.GetAccountSoftCurrency();
            int originalAccountHardCurrency = originalAccount.GetAccountHardCurrency();
            
            //Act
            AccountDbDto accountDbDto = await AccountDbReaderService.ReadAccountAsync(originalAccount.ServiceId);
            
            //Assert
            Assert.IsNotNull(accountDbDto);
            Assert.AreEqual(originalAccount.Username, accountDbDto.Username);
            Assert.AreEqual(originalAccount.ServiceId, accountDbDto.ServiceId);
            Assert.AreEqual(originalAccountRating, accountDbDto.Rating);
            Assert.AreEqual(originalAccountSoftCurrency, accountDbDto.SoftCurrency);
            Assert.AreEqual(originalAccountHardCurrency, accountDbDto.HardCurrency);

            foreach (var warship in originalAccount.Warships)
            {
                WarshipDbDto warshipDbDto = accountDbDto.Warships.Single(w => w.Id == warship.Id);
                int originalWarshipRating = originalAccount.GetWarshipRating(warship.Id);
                int originalWarshipPowerPoints = originalAccount.GetWarshipPowerPoints(warship.Id);
                Assert.AreEqual(originalWarshipRating, warshipDbDto.WarshipRating);
                Assert.AreEqual(originalWarshipPowerPoints, warshipDbDto.WarshipPowerPoints);
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(654)]
        [TestCase(6548481)]
        public async Task MediumAccount(int seedForRandom)
        {
            //Arrange
            string serviceId = "serviceId";
            Account originalAccount = await DefaultAccountFactoryService.CreateDefaultAccountAsync(serviceId);
            int originalAccountRating = originalAccount.GetAccountRating();
            int originalAccountSoftCurrency = originalAccount.GetAccountSoftCurrency();
            int originalAccountHardCurrency = originalAccount.GetAccountHardCurrency();
            
            //Act
            AccountDbDto accountDbDto = await AccountDbReaderService.ReadAccountAsync(originalAccount.ServiceId);
            
            //Assert
            Assert.IsNotNull(accountDbDto);
            Assert.AreEqual(originalAccount.Username, accountDbDto.Username);
            Assert.AreEqual(originalAccount.ServiceId, accountDbDto.ServiceId);
            Assert.AreEqual(originalAccountRating, accountDbDto.Rating);
            Assert.AreEqual(originalAccountSoftCurrency, accountDbDto.SoftCurrency);
            Assert.AreEqual(originalAccountHardCurrency, accountDbDto.HardCurrency);

            foreach (var warship in originalAccount.Warships)
            {
                WarshipDbDto warshipDbDto = accountDbDto.Warships.Single(w => w.Id == warship.Id);
                int originalWarshipRating = originalAccount.GetWarshipRating(warship.Id);
                int originalWarshipPowerPoints = originalAccount.GetWarshipPowerPoints(warship.Id);
                Assert.AreEqual(originalWarshipRating, warshipDbDto.WarshipRating);
                Assert.AreEqual(originalWarshipPowerPoints, warshipDbDto.WarshipPowerPoints);
            }
        }
        
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(654)]
        [TestCase(6548481)]
        public async Task BigAccounts(int seedForRandom)
        {
            //Arrange
            string serviceId = "serviceId";
            Account originalAccount = await DefaultAccountFactoryService.CreateDefaultAccountAsync(serviceId);
            int originalAccountRating = originalAccount.GetAccountRating();
            int originalAccountSoftCurrency = originalAccount.GetAccountSoftCurrency();
            int originalAccountHardCurrency = originalAccount.GetAccountHardCurrency();
            
            //Act
            AccountDbDto accountDbDto = await AccountDbReaderService.ReadAccountAsync(originalAccount.ServiceId);
            
            //Assert
            Assert.IsNotNull(accountDbDto);
            Assert.AreEqual(originalAccount.Username, accountDbDto.Username);
            Assert.AreEqual(originalAccount.ServiceId, accountDbDto.ServiceId);
            Assert.AreEqual(originalAccountRating, accountDbDto.Rating);
            Assert.AreEqual(originalAccountSoftCurrency, accountDbDto.SoftCurrency);
            Assert.AreEqual(originalAccountHardCurrency, accountDbDto.HardCurrency);

            foreach (var warship in originalAccount.Warships)
            {
                WarshipDbDto warshipDbDto = accountDbDto.Warships.Single(w => w.Id == warship.Id);
                int originalWarshipRating = originalAccount.GetWarshipRating(warship.Id);
                int originalWarshipPowerPoints = originalAccount.GetWarshipPowerPoints(warship.Id);
                Assert.AreEqual(originalWarshipRating, warshipDbDto.WarshipRating);
                Assert.AreEqual(originalWarshipPowerPoints, warshipDbDto.WarshipPowerPoints);
            }
        }
    }
}