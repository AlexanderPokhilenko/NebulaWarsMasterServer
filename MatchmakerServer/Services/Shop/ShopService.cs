using System;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NetworkLibrary.NetworkLibrary.Http;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Отвечает за создание и запись модели магазиан в БД
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
            //Такой аккаунт существует?
            Account account = await dbContext.Accounts
                .Where(account1 => account1.ServiceId == playerServiceId)
                .SingleOrDefaultAsync();
            if (account == null)
            {
                throw new Exception("Такой игрок ещё не зарегистрирован");
            }
            
            //Прочитать самую новую модель магазина из БД
            ShopModel shopModelFromDb = await shopModelDbReader.ReadShopModel(account.Id);
            //Создать новую модель магазина
            ShopModel shopModel = await shopFactoryService.Create(playerServiceId);

            ShopModel shopModelWithId;
            //Если модель не сохранена, то записать новую
            if (shopModelFromDb == null)
            {
                Console.WriteLine("Запись первой модели магазина");
                shopModelWithId = await shopWriterService.Write(shopModel, account.Id);
            }
            else
            {
                //Если в БД есть модель, то сравнить содержимое моделей (без Id)
                byte[] arr1 = ZeroFormatterSerializer.Serialize(shopModelFromDb.UiSections);
                byte[] arr2 = ZeroFormatterSerializer.Serialize(shopModel.UiSections);
                //Если модели магазинов отличаются, то в БД нужно сохранить новую
                if (!arr1.SequenceEqual(arr2))
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