using System.Threading.Tasks;
using DataLayer.Tables;
using LibraryForTests;
using NetworkLibrary.NetworkLibrary.Http;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    internal sealed class NotShownRewardsTests : BaseIntegrationFixture
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(26)]
        [TestCase(268)]
        [TestCase(26821)]
        [TestCase(268213)]
        public async Task CorrectReadingTest(int randomSeed)
        {
            //Arrange
            AccountBuilder accountBuilder = new AccountBuilder(randomSeed);
            AccountDirector accountDirector = new BigAccountDirector(accountBuilder, Context);
            accountDirector.WriteToDatabase();
            Account originalAccount = accountDirector.GetAccount();
            int notShownSoftCurrency = accountDirector.GetNotShownSoftCurrency();
            int notShownHardCurrency = accountDirector.GetNotShownHardCurrency();
            int notShownAccountRating = accountDirector.GetNotShownAccountRating();
            int notShownSmallLootboxPoints = accountDirector.GetNotShownSmallLootboxPoints();
            
            //Act
            RewardsThatHaveNotBeenShown result = await NotShownRewardsReaderService
                .GetNotShownResultsAndMarkAsRead(originalAccount.ServiceId);
            
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(notShownSoftCurrency, result.SoftCurrency);
            Assert.AreEqual(notShownAccountRating, result.AccountRating);
            Assert.AreEqual(notShownHardCurrency, result.HardCurrency);
            Assert.AreEqual(notShownSmallLootboxPoints, result.SmallLootboxPoints);
        }
        
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(26)]
        [TestCase(268)]
        [TestCase(26821)]
        [TestCase(268213)]
        public async Task CorrectMarkingTest(int randomSeed)
        {
            //Arrange
            AccountBuilder accountBuilder = new AccountBuilder(randomSeed);
            AccountDirector accountDirector = new BigAccountDirector(accountBuilder, Context);
            accountDirector.WriteToDatabase();
            Account originalAccount = accountDirector.GetAccount();
                        
            //Act
            await NotShownRewardsReaderService
                .GetNotShownResultsAndMarkAsRead(originalAccount.ServiceId);
            
            RewardsThatHaveNotBeenShown result = await NotShownRewardsReaderService
                .GetNotShownResultsAndMarkAsRead(originalAccount.ServiceId);

            
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.SoftCurrency);
            Assert.AreEqual(0, result.AccountRating);
            Assert.AreEqual(0, result.HardCurrency);
            Assert.AreEqual(0, result.SmallLootboxPoints);
        }
    }
}