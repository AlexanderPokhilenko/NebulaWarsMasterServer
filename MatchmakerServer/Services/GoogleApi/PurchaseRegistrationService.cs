using System.Threading.Tasks;
using DataLayer;
using DataLayer.Entities.Transactions.Decrement;
using JetBrains.Annotations;

namespace AmoebaGameMatcherServer.Services.GoogleApi
{
    /// <summary>
    /// Сохраняет в БД данные про покупку за реальные деньги.
    /// </summary>
    public class PurchaseRegistrationService
    {
        private readonly ApplicationDbContext dbContext;

        public PurchaseRegistrationService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> WriteAndGetId(int accountId,  [NotNull] string sku, [NotNull] string googleResponseJson)
        {
            RealPurchaseModel realPurchaseModel = new RealPurchaseModel()
            {
                AccountId = accountId,
                Data = googleResponseJson,
                Sku = sku
            };
            await dbContext.RealPurchaseModels.AddAsync(realPurchaseModel);
            await dbContext.SaveChangesAsync();
            return realPurchaseModel.Id;
        }
    }
}