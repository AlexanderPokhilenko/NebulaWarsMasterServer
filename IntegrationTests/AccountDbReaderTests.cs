using System;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Tables;
using LibraryForTests;
using Microsoft.EntityFrameworkCore;
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
            int accountRating = originalAccount.Warships
                .SelectMany(warship => warship.MatchResultForPlayers)
                .Sum(matchResult => matchResult.WarshipRatingDelta) ?? 0;
            
            Context.Accounts.Add(originalAccount);
            Context.SaveChanges();
            Console.WriteLine(Context.Database.GetDbConnection().Database);
            //Act
            Console.WriteLine(originalAccount.ServiceId);
            Account verifiableAccount = await Service.GetAccount(originalAccount.ServiceId);
            
            //Assert
            Assert.IsNotNull(verifiableAccount);
            Assert.AreEqual(originalAccount.Username, verifiableAccount.Username);
            Assert.AreEqual(accountRating, verifiableAccount.Rating);
        }
    }
}