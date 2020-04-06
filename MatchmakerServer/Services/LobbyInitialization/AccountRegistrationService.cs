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
        private readonly ApplicationDbContext dbContext;

        public AccountRegistrationService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Создаёт аккаунт
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public async Task<bool> TryRegisterAccount(string serviceId)
        {
            Account account = DefaultAccountFactory.CreateDefaultAccount(serviceId);
            await dbContext.Accounts.AddAsync(account);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}