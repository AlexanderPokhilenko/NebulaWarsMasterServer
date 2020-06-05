// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using AmoebaGameMatcherServer.Controllers;
// using DataLayer;
// using DataLayer.Tables;
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
//             var dbContext = dbFactory.Create();
//             NotShownRewardService notShownRewardService = new NotShownRewardService(dbContext);
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
//             Assert.AreEqual(4, results.PremiumCurrency);
//             Assert.AreEqual(34, results.RegularCurrency);
//             Assert.AreEqual(0, results.PointsForBigLootbox);
//             Assert.AreEqual(5, results.PointsForSmallLootbox);
//
//             List<MatchResultForPlayer> matchResultForPlayers = await dbContext.MatchResultForPlayers
//                 .Where(result => result.Warship.AccountId == account.Id)
//                 .ToListAsync();
//             
//             foreach (var matchResultForPlayer in matchResultForPlayers)
//             {
//                 if (matchResultForPlayer.RegularCurrencyDelta != null)
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
//                 ServiceId = UniqueStringFactory.Create(),
//                 Username = "Игорь",
//                 Warships = new List<Warship>
//                 {
//                     new Warship
//                     {
//                         WarshipTypeId = 1,
//                         MatchResultForPlayers = new List<MatchResultForPlayer>
//                         {
//                             new MatchResultForPlayer
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
//                                 RegularCurrencyDelta = 34,
//                                 PointsForBigLootbox = 0,
//                                 PointsForSmallLootbox = 5,
//                                 
//                                 WarshipRatingDelta = 9,
//                                 WasShown = false
//                             },
//                             new MatchResultForPlayer
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
//                                 RegularCurrencyDelta = 12,
//                                 PointsForBigLootbox = 1,
//                                 PointsForSmallLootbox = 2,
//                                 WarshipRatingDelta = 3,
//                                 WasShown = true
//                             },
//                             new MatchResultForPlayer
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
//                                 RegularCurrencyDelta = null,
//                                 PointsForBigLootbox = null,
//                                 PointsForSmallLootbox = null,
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