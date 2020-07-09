using System;
using System.Threading.Tasks;
using DataLayer.Tables;
using JetBrains.Annotations;

namespace AmoebaGameMatcherServer.Services.LobbyInitialization
{
    /// <summary>
    /// Нужен для получения данных про аккаунт из БД при инициализации лобби.
    /// Если такого аккаунта нет в БД, то попытается создать его.
    /// Аккаунт не будет создан, если ServiceId не действителен.
    /// </summary>
    public class AccountFacadeService
    {
        private readonly AccountDbReaderService accountDbReaderService;
        private readonly AccountRegistrationService accountRegistrationService;

        public AccountFacadeService(AccountDbReaderService accountDbReaderService,
            AccountRegistrationService accountRegistrationService)
        {
            this.accountDbReaderService = accountDbReaderService;
            this.accountRegistrationService = accountRegistrationService;
        }
        
        [NotNull]
        public async Task<AccountDbDto> ReadOrCreateAccountAsync([NotNull] string serviceId)
        {
            AccountDbDto account = await accountDbReaderService.ReadAccountAsync(serviceId);
            
            if (account == null)
            {
                Console.WriteLine("Попытка создать аккаунт");
                if (await accountRegistrationService.TryRegisterAccountAsync(serviceId))
                {
                    Console.WriteLine("Успешная регистрация");
                    account = await accountDbReaderService.ReadAccountAsync(serviceId);
                }
                else
                {
                    throw new Exception("Не удалось выполнить регистрацию аккаунта");
                }
            }

            return account;
        }
    }
}