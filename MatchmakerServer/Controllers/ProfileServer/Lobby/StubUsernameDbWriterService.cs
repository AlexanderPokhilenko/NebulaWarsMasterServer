using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer.Controllers
{
    public class StubUsernameDbWriterService
    {
        private readonly ApplicationDbContext dbContext;

        public StubUsernameDbWriterService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task WriteAsync(int accountId, string username)
        {
            Account account = await dbContext.Accounts.FindAsync(accountId);
            account.Username = username;
            await dbContext.SaveChangesAsync();
        }
    }
}