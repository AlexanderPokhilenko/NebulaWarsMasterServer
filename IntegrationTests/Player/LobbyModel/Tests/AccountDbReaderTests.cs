using System.Linq;
using System.Threading.Tasks;
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
            AccountBuilder accountBuilder = new AccountBuilder(2);
            AccountDirector accountDirector = new SmallAccountDirector(accountBuilder, Context);
            accountDirector.WriteToDatabase();
            Account originalAccount = accountDirector.GetAccount();
            int originalAccountRating = accountDirector.GetAccountRating();
            int originalAccountSoftCurrency = accountDirector.GetAccountSoftCurrency();
            int originalAccountHardCurrency = accountDirector.GetAccountHardCurrency();
            
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
                int originalWarshipRating = accountDirector.GetWarshipRating(warship.Id);
                int originalWarshipPowerPoints = accountDirector.GetWarshipPowerPoints(warship.Id);
                Assert.AreEqual(originalWarshipRating, warshipDbDto.WarshipRating);
                Assert.AreEqual(originalWarshipPowerPoints, warshipDbDto.PowerPoints);
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
            AccountBuilder accountBuilder = new AccountBuilder(seedForRandom);
            AccountDirector accountDirector = new MediumAccountDirector(accountBuilder, Context);
            accountDirector.WriteToDatabase();
            Account originalAccount = accountDirector.GetAccount();
            int originalAccountRating = accountDirector.GetAccountRating();
            int originalAccountSoftCurrency = accountDirector.GetAccountSoftCurrency();
            int originalAccountHardCurrency = accountDirector.GetAccountHardCurrency();
            
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
                int originalWarshipRating = accountDirector.GetWarshipRating(warship.Id);
                int originalWarshipPowerPoints = accountDirector.GetWarshipPowerPoints(warship.Id);
                Assert.AreEqual(originalWarshipRating, warshipDbDto.WarshipRating);
                Assert.AreEqual(originalWarshipPowerPoints, warshipDbDto.PowerPoints);
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
            AccountBuilder accountBuilder = new AccountBuilder(seedForRandom);
            AccountDirector accountDirector = new BigAccountDirector(accountBuilder, Context);
            accountDirector.WriteToDatabase();
            Account originalAccount = accountDirector.GetAccount();
            int originalAccountRating = accountDirector.GetAccountRating();
            int originalAccountSoftCurrency = accountDirector.GetAccountSoftCurrency();
            int originalAccountHardCurrency = accountDirector.GetAccountHardCurrency();
            
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
                int originalWarshipRating = accountDirector.GetWarshipRating(warship.Id);
                int originalWarshipPowerPoints = accountDirector.GetWarshipPowerPoints(warship.Id);
                Assert.AreEqual(originalWarshipRating, warshipDbDto.WarshipRating);
                Assert.AreEqual(originalWarshipPowerPoints, warshipDbDto.PowerPoints);
            }
        }
    }
}