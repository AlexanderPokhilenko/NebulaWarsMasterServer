using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services;
using DataLayer;
using DataLayer.Tables;
using MatchmakerTest.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatchmakerTest
{
    [TestClass]
    public class PlayerInfoManagerServiceTests
    {
        /// <summary>
        /// Сервис должен создать аккаунт и вернуть нормальную информацию.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Test1()
        {
            //Arrange
            var dbFactory = new InMemoryDatabaseFactory(nameof(PlayerInfoManagerServiceTests));
            PlayerInfoPullerService playerInfoPullerService = new PlayerInfoPullerService(dbFactory.Create());
            IServiceIdValidator serviceIdValidator = new ServiceIdValidatorServiceStub();
            AccountRegistrationService accountRegistrationService = 
                new AccountRegistrationService(serviceIdValidator, dbFactory.Create());
            PlayerInfoManagerService playerInfoManagerService = 
                new PlayerInfoManagerService(playerInfoPullerService, accountRegistrationService);
            string serviceId = UniqueStringFactory.Create();
            
            //Act
            var accountInfo = await playerInfoManagerService.GetOrCreateAccountInfo(serviceId);
            
            //Assert
            Assert.IsNotNull(accountInfo);
            Assert.IsNotNull(accountInfo.Username);

            var dbContext = dbFactory.Create();
            Account account = await dbContext.Accounts
                .Include(account1 => account1.Warships)
                .ThenInclude(warship => warship.WarshipType)
                .SingleOrDefaultAsync(account1 => account1.ServiceId == serviceId);
            Assert.IsNotNull(account);
            int countOfWarships = account.Warships.Count;
            Assert.AreEqual(2, countOfWarships);
            foreach (var warship in account.Warships)
            {
                Assert.IsNotNull(warship.WarshipType.Name);
            }
        }
    }
}