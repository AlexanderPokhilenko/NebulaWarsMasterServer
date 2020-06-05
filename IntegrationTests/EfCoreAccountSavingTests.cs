using System.Linq;
using DataLayer.Tables;
using LibraryForTests;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    sealed class EfCoreAccountSavingTests : BaseIntegrationFixture
    {
        /// <summary>
        /// Аккаунт нормально сохраняется в БД
        /// </summary>
        /// <returns></returns>
        [Test]
        public void Test1()
        {
            //Arrange
            Account originalAccount = new TestAccountFactory()
                .CreateAccount("egor", "egorService", 2, 9, 8);

            //Act
            Context.Accounts.Add(originalAccount);
            Context.SaveChanges();

            Account account = Context.Accounts
                .Single(account1 => account1.ServiceId == originalAccount.ServiceId);

            //Assert
            Assert.AreEqual(originalAccount.Username, account.Username);
        }
    }
}