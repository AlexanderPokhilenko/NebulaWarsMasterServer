using System.Threading.Tasks;
using DataLayer.Tables;
using LibraryForTests;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    internal sealed class AccountDbReaderTests : BaseIntegrationFixture
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(654)]
        [TestCase(6548481)]
        public async Task Test1(int seedForRandom)
        {
            //Arrange
            AccountBuilder accountBuilder = new AccountBuilder(seedForRandom);
            AccountDirector bigAccountDirector = new BigAccountDirector(accountBuilder);
            bigAccountDirector.Construct();
            Account originalAccount = bigAccountDirector.GetResult();
            int originalAccountRating = bigAccountDirector.GetAccountRating();
            int originalAccountRegularCurrency = bigAccountDirector.GetAccountRegularCurrency();
            int originalAccountPremiumCurrency = bigAccountDirector.GetAccountPremiumCurrency();
            Context.Accounts.Add(originalAccount);
            Context.SaveChanges();
            
            //Act
            Account account = await AccountDbReaderService.GetAccount(originalAccount.ServiceId);
            
            //Assert
            Assert.IsNotNull(account);
            Assert.AreEqual(originalAccount.Username, account.Username);
            Assert.AreEqual(originalAccount.ServiceId, account.ServiceId);
            Assert.AreEqual(originalAccountRating, account.Rating);
            Assert.AreEqual(originalAccountRegularCurrency, account.RegularCurrency);
            Assert.AreEqual(originalAccountPremiumCurrency, account.PremiumCurrency);
        }
        
        [Test]
        public async Task SimpleAccount()
        {
            //Arrange
            AccountBuilder accountBuilder = new AccountBuilder();
            AccountDirector bigAccountDirector = new SmallAccountDirector(accountBuilder);
            bigAccountDirector.Construct();
            Account originalAccount = bigAccountDirector.GetResult();
            int originalAccountRating = bigAccountDirector.GetAccountRating();
            int originalAccountRegularCurrency = bigAccountDirector.GetAccountRegularCurrency();
            int originalAccountPremiumCurrency = bigAccountDirector.GetAccountPremiumCurrency();
            Context.Accounts.Add(originalAccount);
            Context.SaveChanges();
            
            //Act
            Account account = await AccountDbReaderService.GetAccount(originalAccount.ServiceId);
            
            //Assert
            Assert.IsNotNull(account);
            Assert.AreEqual(originalAccount.Username, account.Username);
            Assert.AreEqual(originalAccount.ServiceId, account.ServiceId);
            Assert.AreEqual(originalAccountRating, account.Rating);
            Assert.AreEqual(originalAccountRegularCurrency, account.RegularCurrency);
            Assert.AreEqual(originalAccountPremiumCurrency, account.PremiumCurrency);
        }
    }
}