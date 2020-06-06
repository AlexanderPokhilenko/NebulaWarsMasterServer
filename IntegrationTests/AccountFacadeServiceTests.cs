using System.Threading.Tasks;
using DataLayer.Tables;
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
        public async Task Test1()
        {
            //Arrange
            string serviceId = "someServiceId";
            //Act
            Account account = await AccountFacadeService.ReadOrCreateAccount(serviceId);
            //Assert
            Assert.IsNotNull(account);
        }
    }
}