using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer.Services
{
    public class AccountRegistrationService
    {
        private readonly IServiceIdValidator serviceIdValidatorService;
        private readonly ApplicationDbContext dbContext;

        public AccountRegistrationService(IServiceIdValidator serviceIdValidatorService,
            ApplicationDbContext dbContext)
        {
            this.serviceIdValidatorService = serviceIdValidatorService;
            this.dbContext = dbContext;
        }

        public async Task<bool> TryRegisterAccount(string serviceId)
        {
            if (!serviceIdValidatorService.Validate(serviceId))
            {
                //TODO добавить логгирование
                return false;
            }
            Account account = DefaultAccountFactory.CreateDefaultAccount(serviceId);
            await dbContext.Accounts.AddAsync(account);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}