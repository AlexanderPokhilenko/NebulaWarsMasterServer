using System;
using System.Collections.Generic;
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
            DateTime aDayAgo = DateTime.UtcNow - TimeSpan.FromDays(1);
            ShopModelDb shopModel = await dbContext.ShopModels
                .Where(shopModelDb => shopModelDb.AccountId == accountId && shopModelDb.DateTime > aDayAgo)
                .SingleOrDefaultAsync();

            if (shopModel == null)
            {
                return null;
            }
            
            return ZeroFormatterSerializer.Deserialize<ShopModel>(shopModel.SerializedModel);
        }
    }
}