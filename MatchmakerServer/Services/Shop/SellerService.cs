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
    public class SellerService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ShopTransactionFactory shopTransactionFactory;

        public SellerService(ApplicationDbContext dbContext, ShopTransactionFactory shopTransactionFactory)
        {
            this.dbContext = dbContext;
            this.shopTransactionFactory = shopTransactionFactory;
        }

        public async Task BuyProduct([NotNull] string playerServiceId, int productId)
        {
            Account account = await dbContext.Accounts
                .Where(account1 => account1.ServiceId == playerServiceId)
                .SingleOrDefaultAsync();
            if (account == null)
            {
                throw new Exception("Такого аккаунта не существует");
            }
            
            //Достать последнюю модель магазина
            ShopModelDb shopModelDb = await dbContext
                .ShopModels
                .Include(shopModelDb1 => shopModelDb1.Account)
                .Where(shopModelDb1 => shopModelDb1.Account.ServiceId == playerServiceId)
                .OrderByDescending(shopModelDb1 => shopModelDb1.DateTime)
                .Take(1)
                .SingleOrDefaultAsync();
                

            if (shopModelDb == null)
            {
                throw new Exception("У аккаунта не сохранено ни одной модели магазина");
            }

            ShopModel shopModel;
            try
            {
                shopModel = ZeroFormatterSerializer.Deserialize<ShopModel>(shopModelDb.SerializedModel);
            }
            catch (Exception e)
            {
                throw new Exception("Не удалось десериализовать модель продукта при чтении из БД");
            }

            if (shopModel == null)
            {
                throw new Exception("Не удалось достать модель магазина для игрока");
            }

            ProductModel productModel = shopModel.UiSections
                .SelectMany(uiSection => uiSection.UiItems)
                .SelectMany(arr => arr)
                .SingleOrDefault(productModel1 => productModel1.Id == productId);

            if (productModel == null)
            {
                throw new Exception("В последней версии магазина этого продукта нет");
            }
            
            // создать транзакцию по модели продукта
            Transaction transaction = shopTransactionFactory.Create(productModel, account.Id);
            
            //todo проверить транзакцию на адекватность
            
            
            //записать транзакцию
            await dbContext.Transactions.AddAsync(transaction);
            
            //перезаписать модель продукта
            productModel.Disabled = true;
            byte[] newShopModel = ZeroFormatterSerializer.Serialize(shopModel);
            shopModelDb.SerializedModel = newShopModel;
            
            await dbContext.SaveChangesAsync();
        }
    }
}