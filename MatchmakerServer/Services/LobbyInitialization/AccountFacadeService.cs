using System;
using System.Threading.Tasks;
using DataLayer.Tables;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

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
        public async Task<Account> ReadOrCreateAccount([NotNull] string serviceId)
        {
            Account account = await accountDbReaderService.ReadAccount(serviceId);
            
            if (account == null)
            {
                Console.WriteLine("Попытка создать аккаунт");
                if (await accountRegistrationService.TryRegisterAccount(serviceId))
                {
                    Console.WriteLine("Успешная регистрация");
                    account = await accountDbReaderService.ReadAccount(serviceId);
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