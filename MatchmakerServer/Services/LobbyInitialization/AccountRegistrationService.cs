using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer.Services.LobbyInitialization
{
    /// <summary>
    /// Вызывается во время загрузки данных в лобби, если такого аккаунта нет в БД.
    /// </summary>
    public class AccountRegistrationService
    {
        private readonly DefaultAccountFactoryService defaultAccountFactoryService;

        public AccountRegistrationService(DefaultAccountFactoryService defaultAccountFactoryService)
        {
            this.defaultAccountFactoryService = defaultAccountFactoryService;
        }

        /// <summary>
        /// Создаёт аккаунт
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public async Task<bool> TryRegisterAccountAsync(string serviceId)
        {
            await defaultAccountFactoryService.CreateDefaultAccountAsync(serviceId);
            return true;
        }
    }
}