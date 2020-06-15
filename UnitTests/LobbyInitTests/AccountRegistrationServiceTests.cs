using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatchmakerTest
{
    [TestClass]
    public class AccountRegistrationServiceTests
    {
        /// <summary>
        /// Сервис нормально создаёт аккаунт
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Test1()
        {
            //Arrange
            ApplicationDbContext dbContext = new InMemoryDbContextFactory(nameof(AccountRegistrationServiceTests))
                .Create();
            AccountRegistrationService accountRegistrationService = 
                new AccountRegistrationService(dbContext);
            string serviceId = UniqueStringFactory.Create();
            
            //Act
            bool success = await accountRegistrationService.TryRegisterAccountAsync(serviceId);
            
            //Assert
            Assert.IsTrue(success);
            Account accountDb = await dbContext.Accounts
                .Include(account => account.Warships)
                .SingleOrDefaultAsync(account => account.ServiceId == serviceId);
            Assert.IsNotNull(accountDb);
            int warshipsCount = accountDb.Warships.Count;
            Assert.AreEqual(2, warshipsCount);
        }
    }
}