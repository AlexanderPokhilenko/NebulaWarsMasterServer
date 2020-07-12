using System.Linq;
using System.Threading.Tasks;
using DataLayer.Tables;
using LibraryForTests;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    internal sealed class AccountFacadeServiceTests : BaseIntegrationFixture
    {
        /// <summary>
        /// Сервис создаст аккаунт с таким serviceId, если такого нет в БД.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ServiceCreatesAnAccountIfItIsNot()
        {
            //Arrange
            string serviceId = "someServiceId";
            //Act
            var account = await AccountFacadeService.ReadOrCreateAccountAsync(serviceId);
            var account1 = await Context.Accounts
                .Where(acc => acc.ServiceId == serviceId)
                .SingleAsync();
            
            //Assert
            Assert.IsNotNull(account);
            Assert.IsNotNull(account1);
        }
        
        /// <summary>
        /// Сервис достанет существующий аккаунт, если он есть.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ServiceReadsAccountIfOneExists()
        {
            //Arrange
            string serviceId = "serviceId";
            Account originalAccount = await DefaultAccountFactoryService.CreateDefaultAccountAsync(serviceId); 
            
            //Act
            var account = await AccountFacadeService.ReadOrCreateAccountAsync(originalAccount.ServiceId);
           
            //Assert
            Assert.IsNotNull(account);
            Assert.AreEqual(originalAccount.Id, account.Id);
        }
    }
}