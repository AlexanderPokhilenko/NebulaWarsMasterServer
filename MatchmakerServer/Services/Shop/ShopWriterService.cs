using System;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Controllers
{
    public class ShopWriterService
    {
        private readonly ApplicationDbContext dbContext;

        public ShopWriterService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Write(ShopModel shopModel, int accountId)
        {
            ShopModelDb shopModelDb = new ShopModelDb()
            {
                DateTime = DateTime.UtcNow,
                SerializedModel = ZeroFormatterSerializer.Serialize(shopModel),
                AccountId = accountId
            };
           
            await dbContext.ShopModels.AddAsync(shopModelDb);
            await dbContext.SaveChangesAsync();
        }
    }
}