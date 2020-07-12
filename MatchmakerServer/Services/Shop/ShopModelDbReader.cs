using System;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using NetworkLibrary.NetworkLibrary.Http;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Controllers
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
        public async Task<ShopModel> ReadShopModel(int accountId)
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
            return ZeroFormatterSerializer.Deserialize<ShopModel>(shopModelDb.SerializedModel);
        }
    }
}