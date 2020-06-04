using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    internal sealed class MyTestFixture : BaseIntegrationFixture
    {
        [Test]
         public async Task MyIntegrationTest()
        {
            //Arrange
            int accountRating = 15;
            Account account = new Account()
            {
                Username = "Игорь", 
                Warships = new List<Warship>()
                {
                    new Warship()
                    {
                        PowerLevel = 1,
                        PowerPoints = 1,
                        WarshipType = new WarshipType()
                        {
                            Description = "s",
                            Name = "ss",
                            WarshipCombatRole = new WarshipCombatRole()
                            {
                                Name = "ss"
                            }
                        },
                        MatchResultForPlayers = new List<MatchResultForPlayer>()
                        {
                            new MatchResultForPlayer()
                            {
                                WasShown = true,
                                PremiumCurrencyDelta = 10,
                                RegularCurrencyDelta = 10,
                                WarshipRatingDelta = accountRating,
                                PlaceInMatch = 2,
                                Match = new Match
                                {
                                    StartTime = DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(5)),
                                    FinishTime = DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(5)),
                                    GameServerIp = "1.1.1.1",
                                    GameServerUdpPort = 1
                                }
                            }
                        }
                    }
                },
                ServiceId = "megaIgor",
                CreationDate = DateTime.UtcNow,
            };
            
            Context.Accounts.Add(account);
            Context.SaveChanges();
            
            //Act
            AccountModel accountModel = await Service.GetAccountModel(account.ServiceId);
            
            //Assert
            Assert.IsNotNull(accountModel);
            Assert.AreEqual(account.Username, accountModel.Username);
            Assert.AreEqual(accountRating, accountModel.AccountRating);
        }
    }
}