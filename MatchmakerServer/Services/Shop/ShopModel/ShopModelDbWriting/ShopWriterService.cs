using System;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Services.Shop.ShopModel.ShopModelDbWriting
{
    /// <summary>
    /// Отвечает за сохранение новой модели магазина в БД
    /// </summary>
    public class ShopWriterService
    {
        private readonly ApplicationDbContext dbContext;

        public ShopWriterService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<NetworkLibrary.NetworkLibrary.Http.ShopModel> Write(NetworkLibrary.NetworkLibrary.Http.ShopModel shopModel, int accountId)
        {
            ShopModelDb shopModelDb = new ShopModelDb
            {
                CreationDateTime = DateTime.UtcNow,
                AccountId = accountId
            };
           
            await dbContext.ShopModels.AddAsync(shopModelDb);
            await dbContext.SaveChangesAsync();
            
            //Эта хрень нужна для того, чтобы id у модели магазина всегда был уникальным
            shopModel.Id = shopModelDb.Id;
            shopModelDb.SerializedModel = ZeroFormatterSerializer.Serialize(shopModel);
            await dbContext.SaveChangesAsync();

            return shopModel;
        }
    }
}