using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer.Experimental
{
    /// <summary>
    /// Создаёт новый аккаунт и сохраняет его в БД.
    /// </summary>
    public class DefaultAccountFactoryService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly DefaultAccountFactory defaultAccountFactory;
        private readonly DefaultAccountTransactionsFactory transactionsFactory;

        public DefaultAccountFactoryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            defaultAccountFactory = new DefaultAccountFactory();
            transactionsFactory = new DefaultAccountTransactionsFactory();
        }

        public async Task<Account> CreateDefaultAccountAsync(string playerServiceId)
        {
            Account account = defaultAccountFactory.Create(playerServiceId);

            await dbContext.Accounts.AddAsync(account);
            await dbContext.SaveChangesAsync();

            Transaction resources = transactionsFactory.CreateResourcesTransaction(account);
            Transaction levelsForWarships = transactionsFactory.WarshipLevelsTransaction(account);
            Transaction skinsForWarships = transactionsFactory.CreateSkinsForWarships(account);
            Transaction powerPointsForWarships = transactionsFactory.CreateWarshipsPowerPoints(account);
            
            await dbContext.Transactions.AddAsync(resources);
            await dbContext.Transactions.AddAsync(powerPointsForWarships);
            await dbContext.Transactions.AddAsync(skinsForWarships);
            await dbContext.Transactions.AddAsync(levelsForWarships);
            await dbContext.SaveChangesAsync();
            return account;
        }
    }
}