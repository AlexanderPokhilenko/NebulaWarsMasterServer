using System;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Отвечает за наполнение магазина. Наполнение магазина зависит от набора кораблей в аккаунте.
    /// </summary>
    public class ShopService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ShopWriterService shopWriterService;
        private readonly ShopModelDbReader shopModelDbReader;
        private readonly ShopFactoryService shopFactoryService;

        public ShopService(ShopModelDbReader shopModelDbReader, ShopFactoryService shopFactoryService,
            ShopWriterService shopWriterService, ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.shopModelDbReader = shopModelDbReader;
            this.shopWriterService = shopWriterService;
            this.shopFactoryService = shopFactoryService;
        }

        public async Task<ShopModel> GetShopModelAsync([NotNull] string playerServiceId)
        {
            Account account = await dbContext.Accounts
                .Where(account1 => account1.ServiceId == playerServiceId)
                .SingleOrDefaultAsync();
            if (account == null)
            {
                throw new Exception("Такой игрок ещё не зарегистрирован");
            }
            
            ShopModel shopModel = await shopModelDbReader.ReadShopModel(account.Id);
            if (shopModel == null)
            {
                shopModel = await shopFactoryService.Create(playerServiceId);
                await shopWriterService.Write(shopModel, account.Id);
            }
            
            return shopModel;
        }
    }
}