// using System.Threading.Tasks;
// using AmoebaGameMatcherServer.Services;
// using AmoebaGameMatcherServer.Services.ForControllers;
// using DataLayer;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
//
// namespace MatchmakerTest
// {
//     [TestClass]
//     public class MatchmakerFacadeServiceTest
//     {
//         [TestMethod]
//         public async Task Test1()
//         {
//             //Arrange
//             var inMemoryDatabaseFactory = new InMemoryDbContextFactory(nameof(MatchmakerFacadeServiceTest));
//             var battleRoyaleQueueSingletonService = new BattleRoyaleQueueSingletonService();
//             var dbContext = inMemoryDatabaseFactory.Create();
//             WarshipValidatorService warshipValidatorService = new WarshipValidatorService(dbContext);
//             QueueExtenderService queueExtenderService = new QueueExtenderService(battleRoyaleQueueSingletonService, warshipValidatorService);
//             var unfinishedMatches = new BattleRoyaleUnfinishedMatchesSingletonService();
//             
//             MatchmakerFacadeService matchmaker = new MatchmakerFacadeService(
//                 queueExtenderService,battleRoyaleQueueSingletonService, unfinishedMatches);
//             
//             //Act
//             
//             //Assert
//         }
//     }
// }