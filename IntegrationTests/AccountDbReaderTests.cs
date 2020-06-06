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
            AccountBuilder accountBuilder = new AccountBuilder();
            AccountDirector bigAccountDirector = new SmallAccountDirector(accountBuilder, Context);
            bigAccountDirector.WriteToDatabase();
            Account originalAccount = bigAccountDirector.GetResult();
            int originalAccountRating = bigAccountDirector.GetAccountRating();
            int originalAccountRegularCurrency = bigAccountDirector.GetAccountRegularCurrency();
            int originalAccountPremiumCurrency = bigAccountDirector.GetAccountPremiumCurrency();
            
            //Act
            Account account = await AccountDbReaderService.GetAccount(originalAccount.ServiceId);
            
            //Assert
            Assert.IsNotNull(account);
            Assert.AreEqual(originalAccount.Username, account.Username);
            Assert.AreEqual(originalAccount.ServiceId, account.ServiceId);
            Assert.AreEqual(originalAccountRating, account.Rating);
            Assert.AreEqual(originalAccountRegularCurrency, account.SoftCurrency);
            Assert.AreEqual(originalAccountPremiumCurrency, account.HardCurrency);
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
            Account originalAccount = accountDirector.GetResult();
            
            int originalAccountRating = accountDirector.GetAccountRating();
            int originalAccountRegularCurrency = accountDirector.GetAccountRegularCurrency();
            int originalAccountPremiumCurrency = accountDirector.GetAccountPremiumCurrency();

            //Act
            Account account = await AccountDbReaderService.GetAccount(originalAccount.ServiceId);
            
            //Assert
            Assert.IsNotNull(account);
            Assert.AreEqual(originalAccount.Username, account.Username);
            Assert.AreEqual(originalAccount.ServiceId, account.ServiceId);
            Assert.AreEqual(originalAccountRating, account.Rating);
            Assert.AreEqual(originalAccountRegularCurrency, account.SoftCurrency);
            Assert.AreEqual(originalAccountPremiumCurrency, account.HardCurrency);
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
            Account originalAccount = accountDirector.GetResult();
            int originalAccountRating = accountDirector.GetAccountRating();
            int originalAccountRegularCurrency = accountDirector.GetAccountRegularCurrency();
            int originalAccountPremiumCurrency = accountDirector.GetAccountPremiumCurrency();
            
            //Act
            Account account = await AccountDbReaderService.GetAccount(originalAccount.ServiceId);
            
            //Assert
            Assert.IsNotNull(account);
            Assert.AreEqual(originalAccount.Username, account.Username);
            Assert.AreEqual(originalAccount.ServiceId, account.ServiceId);
            Assert.AreEqual(originalAccountRating, account.Rating);
            Assert.AreEqual(originalAccountRegularCurrency, account.SoftCurrency);
            Assert.AreEqual(originalAccountPremiumCurrency, account.HardCurrency);
        }
    }
}