// ﻿using System.Collections.Generic;
//  using System.Threading.Tasks;
//  using AmoebaGameMatcherServer.Services.MatchCreation;
// using AmoebaGameMatcherServer.Services.PlayerQueueing;
// using AmoebaGameMatcherServer.Services.Queues;
// using DAL;
// using DAL.Tables;
//  using Microsoft.EntityFrameworkCore;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
//
// namespace MatchmakerTest
// {
//     [TestClass]
//     public class MatchDataDbWriterServiceTests
//     {
//         /// <summary>
//         /// При нормальных данных сервис запишет данные о бое в БД.
//         /// </summary>
//         /// <returns></returns>
//         [TestMethod]
//         public async Task Test1()
//         {
//             //Arrange
//             IDbContextFactory dbContextFactory = new InMemoryDbContextFactory(nameof(MatchDataDbWriterServiceTests));
//             var dbContext = dbContextFactory.CreateAsync();
//             MatchDbWriterService matchDbWriterService = new MatchDbWriterService(dbContextFactory);
//             MatchRoutingData matchRoutingData = new MatchRoutingData()
//             {
//                 GameServerIp = "someIp",
//                 GameServerPort = 8918981
//             };
//             var account = TestsAccountFactory.CreateUniqueAccount();
//             await dbContext.Accounts.AddAsync(account);
//             await dbContext.SaveChangesAsync();
//
//             var accountDb = await dbContext.Accounts
//                 .Include(account1 => account1.Warships)
//                     .ThenInclude(warship => warship.WarshipType)
//                 .SingleAsync(account1 => account1.Id == account.Id);
//             
//             var playersBattleInfo = PlayersQueueInfoFactory.CreateSinglePlayer(accountDb);
//             
//             //Act
//             Match match = await matchDbWriterService.WriteMatchDataToDb(matchRoutingData, playersBattleInfo);
//
//             //Assert
//             Assert.IsNotNull(match);
//             Assert.AreEqual(matchRoutingData.GameServerIp, match.GameServerIp);
//             Assert.AreEqual(matchRoutingData.GameServerPort, match.GameServerUdpPort);
//         }
//     }
//
//     public class PlayersQueueInfoFactory
//     {
//         public static List<QueueInfoForPlayer> CreateSinglePlayer(Account accountDb)
//         {
//             throw new System.NotImplementedException();
//         }
//     }
// }