using System;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Tables;
using LibraryForTests;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    internal sealed class AccountDbReaderTests : BaseIntegrationFixture
    {
        [Test]
        public async Task Test1()
        {
            //Arrange
            Account originalAccount = new TestAccountFactory()
                .CreateAccount("egor", "egorService", 2, 9, 8);
            int originalAccountRating = originalAccount.Warships
                .SelectMany(warship => warship.MatchResultForPlayers)
                .Sum(matchResult => matchResult.WarshipRatingDelta) ?? throw new Exception();
            Context.Accounts.Add(originalAccount);
            Context.SaveChanges();
            
            //Act
            Account account = await Service.GetAccount(originalAccount.ServiceId);
            
            //Assert
            Assert.IsNotNull(account);
            Assert.AreEqual(originalAccount.Username, account.Username);
            Assert.AreEqual(originalAccount.ServiceId, account.ServiceId);
            Assert.AreEqual(originalAccountRating, account.Rating);
        }
    }
}