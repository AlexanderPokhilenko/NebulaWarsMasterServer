// using System.Threading.Tasks;
// using AmoebaGameMatcherServer.Services.LobbyInitialization;
// using DAL;
// using DAL.Tables;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
//
// namespace MatchmakerTest
// {
//     [TestClass]
//     public class AccountDbReaderServiceTests
//     {
//         /// <summary>
//         /// Сервис нормально достаёт данные про аккаунт из БД
//         /// </summary>
//         [TestMethod]
//         public async Task Test1()
//         {
//             //Arrange
//             ApplicationDbContext dbContext = new InMemoryDbContextFactory(nameof(AccountDbReaderServiceTests))
//                 .CreateAsync();
//             AccountDbReaderService accountDbReaderService = new AccountDbReaderService(dbContext);
//             Account account = TestsAccountFactory.CreateUniqueAccount();
//             
//             await dbContext.Accounts.AddAsync(account);
//             await dbContext.SaveChangesAsync();
//             
//             //Act
//             var playerInfo = await accountDbReaderService.ReadAccountAsync(account.ServiceId);
//             
//             //Assert
//             Assert.IsNotNull(playerInfo);
//
//             int accountRating = 0;
//             foreach (var warship in account.Warships)
//             {
//                 foreach (var matchResultForPlayer in warship.MatchResults)
//                 {
//                     accountRating += matchResultForPlayer.WarshipRatingDelta ?? 0;
//                 }
//             }
//
//             Assert.AreEqual(account.Username, playerInfo.Username);
//             Assert.AreNotEqual(0, accountRating);
//             Assert.AreEqual(accountRating, playerInfo.AccountRating);
//             Assert.AreEqual(account.HardCurrency, playerInfo.HardCurrency);
//             Assert.AreEqual(account.SoftCurrency, playerInfo.SoftCurrency);
//             Assert.AreEqual(account.LootboxPoints, playerInfo.LootboxPoints);
//         }
//         
//         /// <summary>
//         /// Если аккаунта в БД нет, то сервис вернёт null
//         /// </summary>
//         /// <returns></returns>
//         [TestMethod]
//         public async Task Test2()
//         {
//             //Arrange
//             ApplicationDbContext dbContext = new InMemoryDbContextFactory(nameof(AccountDbReaderServiceTests))
//                 .CreateAsync();
//             AccountDbReaderService accountDbReaderService = new AccountDbReaderService(dbContext);
//             string accountServiceId = "someUniqueId_65461814865468";
//             
//             //Act
//             var playerInfo = await accountDbReaderService.ReadAccountAsync(accountServiceId);
//             
//             //Assert
//             Assert.IsNull(playerInfo);
//         }
//     }
//
//     public class TestsAccountFactory
//     {
//     }
// }