using System;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Services.Shop.ShopModel.ShopModelDbReading
{
    /// <summary>
    /// Читает последнюю модель магазина для игрока
    /// </summary>
    public class ShopModelDbReader
    {
        private readonly ApplicationDbContext dbContext;

        public ShopModelDbReader(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [ItemCanBeNull]
        public async Task<NetworkLibrary.NetworkLibrary.Http.ShopModel> ReadShopModel(int accountId)
        {
            ShopModelDb shopModelDb = await dbContext.ShopModels
                .Where(shopModel1 => shopModel1.AccountId == accountId)
                .OrderBy(shopModel1 => shopModel1.CreationDateTime)
                .FirstOrDefaultAsync();
            if (shopModelDb == null)
            {
                return null;
            }

            if (shopModelDb.SerializedModel == null)
            {
                Console.WriteLine("warning Модель магазина из БД пуста");
                return null;
            }
            
            return ZeroFormatterSerializer
                .Deserialize<NetworkLibrary.NetworkLibrary.Http.ShopModel>(shopModelDb.SerializedModel);
        }
    }
}