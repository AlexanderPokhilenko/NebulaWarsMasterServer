// using System.Threading.Tasks;
// using AmoebaGameMatcherServer.Services.LobbyInitialization;
// using DAL;
// using DAL.Tables;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
//
// namespace MatchmakerTest
// {
//     [TestClass]
//     public class PlayerInfoManagerServiceTests
//     {
//         /// <summary>
//         /// Сервис должен создать аккаунт и вернуть нормальную информацию.
//         /// </summary>
//         /// <returns></returns>
//         [TestMethod]
//         public async Task Test1()
//         {
//             //Arrange
//             var dbFactory = new InMemoryDbContextFactory(nameof(PlayerInfoManagerServiceTests));
//             AccountDbReaderService accountDbReaderService = new AccountDbReaderService(dbFactory.CreateAsync());
//             AccountRegistrationService accountRegistrationService = 
//                 new AccountRegistrationService(dbFactory.CreateAsync());
//             AccountFacadeService accountFacadeService = 
//                 new AccountFacadeService(accountDbReaderService, accountRegistrationService);
//             string serviceId = UniqueStringFactory.CreateAsync();
//             
//             //Act
//             var accountInfo = await accountFacadeService.ReadOrCreateAccountAsync(serviceId);
//             
//             //Assert
//             Assert.IsNotNull(accountInfo);
//             Assert.IsNotNull(accountInfo.Username);
//
//             var dbContext = dbFactory.CreateAsync();
//             Account account = await dbContext.Accounts
//                 .Include(account1 => account1.Warships)
//                 .ThenInclude(warship => warship.WarshipType)
//                 .SingleOrDefaultAsync(account1 => account1.ServiceId == serviceId);
//             Assert.IsNotNull(account);
//             int countOfWarships = account.Warships.Count;
//             Assert.AreEqual(2, countOfWarships);
//             foreach (var warship in account.Warships)
//             {
//                 Assert.IsNotNull(warship.WarshipType.Name);
//             }
//         }
//     }
// }