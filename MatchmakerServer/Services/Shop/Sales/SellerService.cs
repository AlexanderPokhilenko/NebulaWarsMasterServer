using System;
using System.Linq;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using NetworkLibrary.NetworkLibrary.Http;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Services.Shop.Sales
{ 
    /// <summary>
    /// Отвечает за создание транзакций при покупке товаров. 
    /// </summary>
    public class SellerService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ShopTransactionFactory shopTransactionFactory;

        public SellerService(ApplicationDbContext dbContext, ShopTransactionFactory shopTransactionFactory)
        {
            this.dbContext = dbContext;
            this.shopTransactionFactory = shopTransactionFactory;
        }

        public async Task BuyProduct(string playerServiceId, int productId, string base64ProductModelFromClient,
            int shopModelId)
        {
            //Аккаунт существует?
            Account account = await dbContext.Accounts
                .Where(account1 => account1.ServiceId == playerServiceId)
                .SingleOrDefaultAsync();
            if (account == null)
            {
                throw new Exception($"Такого аккаунта не существует {nameof(playerServiceId)} {playerServiceId}");
            }
            
            //Модель магазина существует?
            ShopModelDb shopModelDb = await dbContext
                .ShopModels
                .Where(shopModelDb1 => shopModelDb1.Id == shopModelId)
                .SingleOrDefaultAsync();
            if (shopModelDb == null)
            {
                throw new Exception($"Такой модели магазина не существует {nameof(shopModelId)} {shopModelId}");
            }

            //Эта модель создана для этого аккаунта?
            if (account.Id != shopModelDb.AccountId)
            {
                throw new Exception("Модель магазина не относится к этому аккаунту");
            }
            
            //Эта модель не просрочена?
            if (DateTime.UtcNow - shopModelDb.CreationDateTime > TimeSpan.FromDays(3))
            {
                throw new Exception("Модель магазина просрочена");
            }

            //В модели магазина из БД есть продукт с таким же id?
            NetworkLibrary.NetworkLibrary.Http.ShopModel shopModel;
            try
            {
                shopModel = ZeroFormatterSerializer.Deserialize<NetworkLibrary.NetworkLibrary.Http.ShopModel>(shopModelDb.SerializedModel);
            }
            catch
            {
                throw new Exception("Не удалось десериализовать модель продукта при чтении из БД");
            }
            if (shopModel == null)
            {
                throw new Exception("Не удалось достать модель магазина для игрока");
            }

            ProductModel productModelFromDb = shopModel.UiSections
                .SelectMany(uiSection => uiSection.UiItems)
                .SelectMany(arr => arr)
                .SingleOrDefault(productModel1 => productModel1.Id == productId);
            if (productModelFromDb == null)
            {
                throw new Exception("В модели магазина такого продукта нет.");
            }
            
            //Продукт из БД полностью совпадает с присланным с клиента?
            byte[] serializedProductModelFromClient = Convert.FromBase64String(base64ProductModelFromClient);
            byte[] serializedProductModelFromDb = ZeroFormatterSerializer.Serialize(productModelFromDb);
            if (!serializedProductModelFromClient.SequenceEqual(serializedProductModelFromDb))
            {
                Console.WriteLine("клиент");
                ProductModel productModelFromClient =
                    ZeroFormatterSerializer.Deserialize<ProductModel>(serializedProductModelFromClient);
               

                Console.WriteLine(productModelFromClient.ToString());
                Console.WriteLine(productModelFromDb.ToString());
                throw new Exception("Модели продуктов не совпадают");
            }

            //создать транзакцию по модели продукта
            Transaction transaction = shopTransactionFactory.Create(productModelFromDb, account.Id);
            
            //todo проверить транзакцию на адекватность
            
            //записать транзакцию
            await dbContext.Transactions.AddAsync(transaction);
            
            //перезаписать модель продукта
            productModelFromDb.IsDisabled = true;
            byte[] newShopModel = ZeroFormatterSerializer.Serialize(shopModel);
            shopModelDb.SerializedModel = newShopModel;
            
            await dbContext.SaveChangesAsync();
        }
    }
}