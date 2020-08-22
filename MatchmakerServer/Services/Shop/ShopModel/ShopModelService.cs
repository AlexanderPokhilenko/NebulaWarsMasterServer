using System;
using System.Linq;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.Shop.ShopModel.ShopModelCreation;
using AmoebaGameMatcherServer.Services.Shop.ShopModel.ShopModelDbReading;
using AmoebaGameMatcherServer.Services.Shop.ShopModel.ShopModelDbWriting;
using DataLayer;
using DataLayer.Tables;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Services.Shop.ShopModel
{
    /// <summary>
    /// Отвечает за создание и запись модели магазина в БД
    /// </summary>
    public class ShopService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly SectionModelsComparator comparator;
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
            comparator = new SectionModelsComparator();
        }

        public async Task<NetworkLibrary.NetworkLibrary.Http.ShopModel> GetShopModelAsync([NotNull] string playerServiceId)
        {
            //Такой аккаунт существует?
            Account account = await dbContext.Accounts
                .Where(account1 => account1.ServiceId == playerServiceId)
                .SingleOrDefaultAsync();
            if (account == null)
            {
                throw new Exception("Такой игрок ещё не зарегистрирован");
            }
            
            //Прочитать самую новую модель магазина из БД
            NetworkLibrary.NetworkLibrary.Http.ShopModel shopModelFromDb = await shopModelDbReader
                .ReadShopModel(account.Id);
            //Создать новую модель магазина
            NetworkLibrary.NetworkLibrary.Http.ShopModel shopModel = await shopFactoryService.Create(playerServiceId);

            NetworkLibrary.NetworkLibrary.Http.ShopModel shopModelWithId;
            //Если модель не сохранена, то записать новую
            if (shopModelFromDb == null)
            {
                Console.WriteLine("Запись первой модели магазина");
                shopModelWithId = await shopWriterService.Write(shopModel, account.Id);
            }
            else
            {
                //Если в БД есть модель, то сравнить содержимое моделей (без Id)
                bool needToReplace = comparator.NeedToReplace(shopModelFromDb.UiSections, shopModel.UiSections);
                
                //Если модели магазинов отличаются, то в БД нужно сохранить новую
                if (needToReplace)
                {
                    Console.WriteLine("Модели отличаются");
                    shopModelWithId = await shopWriterService.Write(shopModel, account.Id);
                }
                else
                {
                    Console.WriteLine("Модели совпадают");
                    //Если модели совпадают, то записывать в БД ничего не нужно
                    shopModelWithId = shopModelFromDb;
                }
            }

            return shopModelWithId;
        }
    }
}