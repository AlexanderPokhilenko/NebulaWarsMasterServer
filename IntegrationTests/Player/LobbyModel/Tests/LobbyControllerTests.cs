using System;
using System.Threading.Tasks;
using DataLayer.Tables;
using IntegrationTests.Player.LobbyModel.Config;
using LibraryForTests;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ZeroFormatter;

namespace IntegrationTests.Player.LobbyModel.Tests
{
    [TestFixture]
    internal sealed class LobbyControllerTests : BaseIntegrationFixture
    {
        /// <summary>
        /// Сервис создаст аккаунт с таким serviceId, если такого нет в БД,
        /// и вернёт заполненный LobbyModel
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ServiceCreatesAnAccountIfItIsNot()
        {
            //Arrange
            string serviceId = "someServiceId";
            //Act
            ActionResult<string> resultObj = await LobbyModelController.Create(serviceId, null);
            string base64String = resultObj.Value;
            byte[] data = Convert.FromBase64String(base64String);
            NetworkLibrary.NetworkLibrary.Http.LobbyModel lobbyModel = ZeroFormatterSerializer.Deserialize<NetworkLibrary.NetworkLibrary.Http.LobbyModel>(data);
            
            //Assert
            Assert.IsNotNull(lobbyModel);
            Assert.IsNotNull(lobbyModel.AccountDto);
            Assert.IsNotNull(lobbyModel.AccountDto.Warships);
            Assert.IsTrue(lobbyModel.AccountDto.Warships.Count>1);
            Assert.IsNotNull(lobbyModel.WarshipRatingScaleModel);
            Assert.IsNotNull(lobbyModel.RewardsThatHaveNotBeenShown);

            foreach (var warshipDto in lobbyModel.AccountDto.Warships)
            {
                Assert.IsNotNull(warshipDto.Description);
                Assert.IsNotNull(warshipDto.WarshipName);
                Assert.IsNotNull(warshipDto.CombatRoleName);
            }
        }
         
        /// <summary>
        /// Сервис нормально прочитает аккаунт, который есть в БД, и отобразит в сериализуемый класс
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ServiceReadsAccountIfOneExists()
        {
            //Arrange
            string serviceId = "serviceId";
            Account originalAccount = await DefaultAccountFactoryService.CreateDefaultAccountAsync(serviceId);
            int originalAccountRating = originalAccount.GetAccountRating();
            int accountSoftCurrency = originalAccount.GetAccountSoftCurrency();
            int accountHardCurrency = originalAccount.GetAccountHardCurrency();
            //Act
            NetworkLibrary.NetworkLibrary.Http.LobbyModel lobbyModel = await LobbyModelFacadeService.CreateAsync(originalAccount.ServiceId);
            
            //Assert
            Assert.AreEqual(originalAccountRating, lobbyModel.AccountDto.AccountRating);
            Assert.AreEqual(accountSoftCurrency, lobbyModel.AccountDto.SoftCurrency);
            Assert.AreEqual(accountHardCurrency, lobbyModel.AccountDto.HardCurrency);
            Assert.IsNotNull(lobbyModel);
            Assert.IsNotNull(lobbyModel.AccountDto);
            Assert.IsNotNull(lobbyModel.AccountDto.Warships);
            Assert.IsTrue(lobbyModel.AccountDto.Warships.Count>1);
            Assert.IsNotNull(lobbyModel.WarshipRatingScaleModel);
            Assert.IsNotNull(lobbyModel.RewardsThatHaveNotBeenShown);

            foreach (var warshipDto in lobbyModel.AccountDto.Warships)
            {
                Assert.IsNotNull(warshipDto.Description);
                Assert.IsNotNull(warshipDto.WarshipName);
                Assert.IsNotNull(warshipDto.CombatRoleName);
            }
        }
    }
}