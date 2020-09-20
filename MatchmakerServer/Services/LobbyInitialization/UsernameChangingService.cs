using System;
using System.Linq;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Controllers.ProfileServer.Lobby;
using DataLayer;
using DataLayer.Tables;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.LobbyInitialization
{
    public class UsernameChangingService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UsernameValidatorService usernameValidatorService;

        public UsernameChangingService(UsernameValidatorService usernameValidatorService, 
            ApplicationDbContext dbContext)
        {
            this.usernameValidatorService = usernameValidatorService;
            this.dbContext = dbContext;
        }
        
        public async Task<UsernameValidationResultEnum> ChangeUsername([NotNull] string playerServiceId, [NotNull] string username)
        {
            var validationResult = usernameValidatorService.IsUsernameValid(username); 
            if (validationResult == UsernameValidationResultEnum.Ok)
            {
                Account account = await dbContext.Accounts
                    .Where(item => item.ServiceId == playerServiceId)
                    .SingleOrDefaultAsync();

                if (account == null)
                {
                    Console.WriteLine("Такого аккаунта нет.");
                }

                if (account.Username == username)
                {
                    Console.WriteLine("Такой username уже установлен");
                }
                
                account.Username = username;
                await dbContext.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("username не соответствует требованиям "+validationResult);
            }

            return validationResult;
        }
    }
}