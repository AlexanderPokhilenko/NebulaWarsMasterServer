using AmoebaGameMatcherServer.Services.LobbyInitialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkLibrary.NetworkLibrary.Http;

namespace MatchmakerTest.LobbyInitTests
{
    [TestClass]
    public class UsernameTests
    {
        private readonly UsernameValidatorService usernameValidatorService = new UsernameValidatorService();
        
        [TestMethod]
        public void Test1()
        {
            string username = "53326d";
            var resultEnum = usernameValidatorService.IsUsernameValid(username);
            Assert.AreEqual(UsernameValidationResultEnum.DoesNotBeginWithALetter, resultEnum);
        }
        
        [TestMethod]
        public void Test2()
        {
            string username = "a0123456789";
            var resultEnum = usernameValidatorService.IsUsernameValid(username);
            Assert.AreEqual(UsernameValidationResultEnum.Ok, resultEnum);
        }   
        
        [TestMethod]
        public void Test3()
        {
            string username = "020";
            var resultEnum = usernameValidatorService.IsUsernameValid(username);
            Assert.AreEqual(UsernameValidationResultEnum.TooShort, resultEnum);
        }
        
        [TestMethod]
        public void Test4()
        {
            string username = "w+62+62+62+62";
            var resultEnum = usernameValidatorService.IsUsernameValid(username);
            Assert.AreEqual(UsernameValidationResultEnum.InvalidCharacter, resultEnum);
        } 
    }
}