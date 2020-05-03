using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using DataLayer;
using DataLayer.Tables;
using MatchmakerTest.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkLibrary.NetworkLibrary.Http;

namespace MatchmakerTest
{
    [TestClass]
    public class LinqSumTests
    {
        /// <summary>
        /// Сервис, который читает информацию о аккаунте с нулевым рейтингом не бросат исключения
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Test1()
        {
            //Arrange
            var inMemoryDbContextFactory = new InMemoryDbContextFactory(nameof(LinqSumTests));
            ApplicationDbContext dbContext = inMemoryDbContextFactory.Create();
            Account account = CreateAccount();
            await dbContext.Accounts.AddAsync(account);
            await dbContext.SaveChangesAsync();
            //Act
            
            AccountDbReaderService accountDbReaderService = new AccountDbReaderService(dbContext);
            AccountModel result = await accountDbReaderService.GetAccountInfo(account.ServiceId);
            
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.AccountRating);
        }

        /// <summary>
        /// Аккаунт с одним кораблём без боёв.
        /// </summary>
        /// <returns></returns>
        private Account CreateAccount()
        {
            return new Account()
            {
                Username = "user",
                PremiumCurrency = 2,
                ServiceId = UniqueStringFactory.Create(),
                CreationDate = DateTime.Now,
                RegularCurrency = 24,
                PointsForBigLootbox = 54,
                PointsForSmallLootbox = 5,
                Warships = new List<Warship>()
                {
                    new Warship()
                    {
                        WarshipTypeId = 1,
                        CombatPowerLevel = 6,
                        CombatPowerValue = 654
                    }
                }
            };
        }
    }
}