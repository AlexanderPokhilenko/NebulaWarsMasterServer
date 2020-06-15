using System.Threading.Tasks;
using AmoebaGameMatcherServer.Controllers;
using DataLayer;
using DataLayer.Tables;
using MatchmakerTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmoebaGameMatcherServer.Services.Lootbox
{
    [TestClass]
    public class LootboxTests
    {
        [TestMethod]
        public async Task Test1()
        {
            //Arrange
            var dbContextFactory = new InMemoryDbContextFactory(nameof(LootboxTests));
            var dbContext = dbContextFactory.Create();
            var service = new SmallLootboxOpenAllowingService(dbContext);
            string playerId = UniqueStringFactory.Create();
            Account account = new Account()
            {
                ServiceId = playerId,
                Username = playerId
            };
            dbContext.Accounts.Add(account);
            dbContext.SaveChanges();
            
            //Act
            bool result = await service.CanPlayerOpenLootboxAsync(playerId);

            //Assert
            Assert.IsFalse(result);
        }
    }
}