using System.Threading.Tasks;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
    /// <summary>
    /// Нужен для получения данных про аккаунт из БД. Если такого аккаунта нет в БД, то попытается создать его.
    /// Аккаунт не будет создан, если ServiceId не действителен.
    /// </summary>
    public class PlayerInfoManagerService
    {
        private readonly PlayerInfoPullerService playerInfoPullerService;
        private readonly AccountRegistrationService accountRegistrationService;

        public PlayerInfoManagerService(PlayerInfoPullerService playerInfoPullerService,
            AccountRegistrationService accountRegistrationService)
        {
            this.playerInfoPullerService = playerInfoPullerService;
            this.accountRegistrationService = accountRegistrationService;
        }
        
        [ItemCanBeNull]
        public async Task<AccountInfo> GetOrCreateAccountInfo([NotNull] string serviceId)
        {
            AccountInfo accountInfo = await playerInfoPullerService.GetPlayerInfo(serviceId);
            
            if (accountInfo == null)
            {
                if (await accountRegistrationService.TryRegisterAccount(serviceId))
                {
                    accountInfo = await playerInfoPullerService.GetPlayerInfo(serviceId);
                }
                else
                {
                    return null;
                }
            }

            return accountInfo;
        }
    }
}