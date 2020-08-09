using System.Collections.Generic;
using System.Linq;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.LobbyInitialization
{
    public class UsernameValidatorService
    {
        private readonly HashSet<char> numbers =  "0123456789".ToHashSet();
        private readonly HashSet<char> alphabet ="ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower().ToHashSet();
        
        public UsernameValidationResultEnum IsUsernameValid(string username)
        {
            if (username.Length <= 4)
            {
                return UsernameValidationResultEnum.TooShort;
            }
            
            if (21 <= username.Length)
            {
                return UsernameValidationResultEnum.TooLong;
            }

            if (username.Contains(' '))
            {
                return UsernameValidationResultEnum.ContainsSpace;
            }

            username = username.ToLower();
            if (!alphabet.Contains(username.First()))
            {
                return UsernameValidationResultEnum.DoesNotBeginWithALetter;
            }

            HashSet<char> usernameSet = new HashSet<char>(username);
            usernameSet.ExceptWith(alphabet);
            usernameSet.ExceptWith(numbers);
            if (usernameSet.Count != 0)
            {
                return UsernameValidationResultEnum.InvalidCharacter;
            }

            return UsernameValidationResultEnum.Ok;
        }
    }
}