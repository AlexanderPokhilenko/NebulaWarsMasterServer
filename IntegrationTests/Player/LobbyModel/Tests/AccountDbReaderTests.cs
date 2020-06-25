// using System.Linq;
// using System.Threading.Tasks;
// using DataLayer.Tables;
// using LibraryForTests;
// using NUnit.Framework;
//
// namespace IntegrationTests
// {
//     [TestFixture]
//     internal sealed class AccountDbReaderTests : BaseIntegrationFixture
//     {
//         [Test]
//         public async Task SmallAccount()
//         {
//             //Arrange
//             AccountBuilder accountBuilder = new AccountBuilder(2);
//             AccountDirector accountDirector = new SmallAccountDirector(accountBuilder, Context);
//             accountDirector.WriteToDatabase();
//             Account originalAccount = accountDirector.GetAccount();
//             int originalAccountRating = accountDirector.GetAccountRating();
//             int originalAccountRegularCurrency = accountDirector.GetAccountSoftCurrency();
//             int originalAccountPremiumCurrency = accountDirector.GetAccountHardCurrency();
//             
//             //Act
//             var account = await AccountDbReaderService.ReadAccountAsync(originalAccount.ServiceId);
//             
//             //Assert
//             Assert.IsNotNull(account);
//             Assert.AreEqual(originalAccount.Username, account.Username);
//             Assert.AreEqual(originalAccount.ServiceId, account.ServiceId);
//             Assert.AreEqual(originalAccountRating, account.Rating);
//             Assert.AreEqual(originalAccountRegularCurrency, account.SoftCurrency);
//             Assert.AreEqual(originalAccountPremiumCurrency, account.HardCurrency);
//             
//             
//             //TODO заменить это
//             foreach (var warship in account.Warships)
//             {
//                 Warship originalWarship = originalAccount.Warships.Single(w => w.Id == warship.Id);
//                 int originalWarshipRating = originalWarship.MatchResults.Sum(mr => mr.WarshipRatingDelta);
//                 // int originalWarshipPowerPoints = originalWarship.WarshipPowerPoints.Sum(wpp => wpp.Quantity);
//                 Assert.AreEqual(originalWarshipRating, warship.WarshipRating);
//                 // Assert.AreEqual(originalWarshipPowerPoints, warship.PowerPoints);
//             }
//         }
//
//         [TestCase(1)]
//         [TestCase(2)]
//         [TestCase(3)]
//         [TestCase(4)]
//         [TestCase(5)]
//         [TestCase(6)]
//         [TestCase(654)]
//         [TestCase(6548481)]
//         public async Task MediumAccount(int seedForRandom)
//         {
//             //Arrange
//             AccountBuilder accountBuilder = new AccountBuilder(seedForRandom);
//             AccountDirector accountDirector = new MediumAccountDirector(accountBuilder, Context);
//             accountDirector.WriteToDatabase();
//             Account originalAccount = accountDirector.GetAccount();
//             
//             int originalAccountRating = accountDirector.GetAccountRating();
//             int originalAccountRegularCurrency = accountDirector.GetAccountSoftCurrency();
//             int originalAccountPremiumCurrency = accountDirector.GetAccountHardCurrency();
//
//             //Act
//             var account = await AccountDbReaderService.ReadAccountAsync(originalAccount.ServiceId);
//             
//             //Assert
//             Assert.IsNotNull(account);
//             Assert.AreEqual(originalAccount.Username, account.Username);
//             Assert.AreEqual(originalAccount.ServiceId, account.ServiceId);
//             Assert.AreEqual(originalAccountRating, account.Rating);
//             Assert.AreEqual(originalAccountRegularCurrency, account.SoftCurrency);
//             Assert.AreEqual(originalAccountPremiumCurrency, account.HardCurrency);
//
//             //TODO заменить это
//             foreach (var warship in account.Warships)
//             {
//                 Warship originalWarship = originalAccount.Warships.Single(w => w.Id == warship.Id);
//                 int originalWarshipRating = originalWarship.MatchResults.Sum(mr => mr.WarshipRatingDelta);
//                 // int originalWarshipPowerPoints = originalWarship.WarshipPowerPoints.Sum(wpp => wpp.Quantity);
//                 Assert.AreEqual(originalWarshipRating, warship.WarshipRating);
//                 // Assert.AreEqual(originalWarshipPowerPoints, warship.PowerPoints);
//             }
//         }
//         
//         [TestCase(1)]
//         [TestCase(2)]
//         [TestCase(3)]
//         [TestCase(4)]
//         [TestCase(5)]
//         [TestCase(6)]
//         [TestCase(654)]
//         [TestCase(6548481)]
//         public async Task BigAccounts(int seedForRandom)
//         {
//             //Arrange
//             AccountBuilder accountBuilder = new AccountBuilder(seedForRandom);
//             AccountDirector accountDirector = new BigAccountDirector(accountBuilder, Context);
//             accountDirector.WriteToDatabase();
//             Account originalAccount = accountDirector.GetAccount();
//             int originalAccountRating = accountDirector.GetAccountRating();
//             int originalAccountRegularCurrency = accountDirector.GetAccountSoftCurrency();
//             int originalAccountPremiumCurrency = accountDirector.GetAccountHardCurrency();
//             
//             //Act
//             var account = await AccountDbReaderService.ReadAccountAsync(originalAccount.ServiceId);
//             
//             //Assert
//             Assert.IsNotNull(account);
//             Assert.AreEqual(originalAccount.Username, account.Username);
//             Assert.AreEqual(originalAccount.ServiceId, account.ServiceId);
//             Assert.AreEqual(originalAccountRating, account.Rating);
//             Assert.AreEqual(originalAccountRegularCurrency, account.SoftCurrency);
//             Assert.AreEqual(originalAccountPremiumCurrency, account.HardCurrency);
//             
//             //TODO заменить это
//             foreach (var warship in account.Warships)
//             {
//                 Warship originalWarship = originalAccount.Warships.Single(w => w.Id == warship.Id);
//                 int originalWarshipRating = originalWarship.MatchResults.Sum(mr => mr.WarshipRatingDelta);
//                 // int originalWarshipPowerPoints = originalWarship.WarshipPowerPoints.Sum(wpp => wpp.Quantity);
//                 Assert.AreEqual(originalWarshipRating, warship.WarshipRating);
//                 // Assert.AreEqual(originalWarshipPowerPoints, warship.PowerPoints);
//             }
//         }
//     }
// }