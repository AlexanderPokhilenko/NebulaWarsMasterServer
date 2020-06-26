// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using AmoebaGameMatcherServer.Controllers;
// using DAL;
// using DAL.Tables;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
//
// namespace MatchmakerTest
// {
//     [TestClass]
//     public class NotShownRewardDbUpdaterServiceTests
//     {
//         /// <summary>
//         /// Сервис правильно достаёт данные из бд и помечает их как прочитанные.
//         /// </summary>
//         /// <returns></returns>
//         [TestMethod]
//         public async Task Test1()
//         {
//             //Arrange
//             var dbFactory = new InMemoryDbContextFactory(nameof(NotShownRewardDbUpdaterServiceTests));
//             var dbContext = dbFactory.CreateAsync();
//             NotShownRewardsReaderService notShownRewardService = new NotShownRewardsReaderService(dbContext);
//
//             Account account = CreateAccount(); 
//             
//             await dbContext.Accounts.AddAsync(account);
//             await dbContext.SaveChangesAsync();
//
//             //Act
//             var results = await notShownRewardService
//                 .GetNotShownRewards(account.ServiceId);
//
//             //Assert
//             Assert.IsNotNull(results);
//             Assert.AreEqual(4, results.HardCurrencyDelta);
//             Assert.AreEqual(34, results.SoftCurrency);
//             Assert.AreEqual(0, results.BigLootboxPoints);
//             Assert.AreEqual(5, results.LootboxPoints);
//
//             List<MatchResultDto> matchResultForPlayers = await dbContext.MatchResults
//                 .Where(result => result.Warship.AccountId == account.Id)
//                 .ToListAsync();
//             
//             foreach (var matchResultForPlayer in matchResultForPlayers)
//             {
//                 if (matchResultForPlayer.SoftCurrency != null)
//                 {
//                     Assert.IsTrue(matchResultForPlayer.WasShown);
//                 }
//             }
//         }
//
//         private Account CreateAccount()
//         {
//             return new Account
//             {
//                 ServiceId = UniqueStringFactory.CreateAsync(),
//                 Username = "Игорь",
//                 Warships = new List<Warship>
//                 {
//                     new Warship
//                     {
//                         WarshipTypeId = 1,
//                         MatchResults = new List<MatchResultDto>
//                         {
//                             new MatchResultDto
//                             {
//                                 Match = new Match
//                                 {
//                                     StartTime = DateTime.Now,
//                                     FinishTime = DateTime.Now + TimeSpan.FromMinutes(5),
//                                     GameServerIp = "someIp",
//                                     GameServerUdpPort = 668
//                                 },
//                                 PlaceInMatch = 2,
//                                 PremiumCurrencyDelta = 4,
//                                 SoftCurrency = 34,
//                                 BigLootboxPoints = 0,
//                                 LootboxPoints = 5,
//                                 
//                                 WarshipRatingDelta = 9,
//                                 WasShown = false
//                             },
//                             new MatchResultDto
//                             {
//                                 Match = new Match
//                                 {
//                                     StartTime = DateTime.Now,
//                                     FinishTime = DateTime.Now + TimeSpan.FromMinutes(5),
//                                     GameServerIp = "someIp",
//                                     GameServerUdpPort = 668
//                                 },
//                                 PlaceInMatch = 4,
//                                 PremiumCurrencyDelta = 0,
//                                 SoftCurrency = 12,
//                                 BigLootboxPoints = 1,
//                                 LootboxPoints = 2,
//                                 WarshipRatingDelta = 3,
//                                 WasShown = true
//                             },
//                             new MatchResultDto
//                             {
//                                 Match = new Match
//                                 {
//                                     StartTime = DateTime.Now,
//                                     FinishTime = null,
//                                     GameServerIp = "someIp",
//                                     GameServerUdpPort = 668
//                                 },
//                                 PlaceInMatch = null,
//                                 PremiumCurrencyDelta = null,
//                                 SoftCurrency = null,
//                                 BigLootboxPoints = null,
//                                 LootboxPoints = null,
//                                 WarshipRatingDelta = null,
//                                 WasShown = false
//                             },
//                         }
//                     }
//                 }
//             };
//         }
//     }
// }