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
    public class ProductChecker
    {
        public bool IsEqual(ProductModel productModel1, ProductModel productModel2)
        {
            if (productModel1.Id != productModel2.Id)
            {
                // Console.WriteLine("Id");
                return false;
            }

            // Console.WriteLine(productModel1.Id+" "+productModel2.Id);

            if (productModel1.ResourceTypeEnum != productModel2.ResourceTypeEnum)
            {
                // Console.WriteLine("ResourceTypeEnum");
                return false;
            }

            // Console.WriteLine(productModel1.ResourceTypeEnum+" "+productModel2.ResourceTypeEnum);

            if (productModel1.SerializedModel.Length != productModel2.SerializedModel.Length)
            {
                // Console.WriteLine("SerializedModel.Length");
                return false;
            }

            // Console.WriteLine(productModel1.SerializedModel.Length+" "+productModel2.SerializedModel.Length);

            switch (productModel1.ResourceTypeEnum)
            {
                case ResourceTypeEnum.WarshipPowerPoints:
                    var model1 = ZeroFormatterSerializer.Deserialize<WarshipPowerPointsProductModel>(productModel1.SerializedModel);
                    var model2 = ZeroFormatterSerializer.Deserialize<WarshipPowerPointsProductModel>(productModel2.SerializedModel);
                    
                    if (model1.FinishValue!=model2.FinishValue)
                    {
                        // Console.WriteLine("FinishValue ");
                        return false;
                    }
                    if (model1.StartValue!=model2.StartValue)
                    {
                        // Console.WriteLine("StartValue ");
                        return false;
                    }
                    if (model1.WarshipId!=model2.WarshipId)
                    {
                        // Console.WriteLine("WarshipId");
                        return false;
                    }
                    if (model1.WarshipSkinName!=model2.WarshipSkinName)
                    {
                        // Console.WriteLine("WarshipSkinName");
                        return false;
                    }
                    if (model1.MaxValueForLevel!=model2.MaxValueForLevel)
                    {
                        // Console.WriteLine("MaxValueForLevel");
                        return false;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            
            if (productModel1.CostModel.CostTypeEnum != productModel2.CostModel.CostTypeEnum)
            {
                // Console.WriteLine("CostTypeEnum");
                return false;
            }

            // Console.WriteLine(productModel1.SerializedModel.Length+" "+productModel2.SerializedModel.Length);

            if (productModel1.CostModel.SerializedCostModel.Length != productModel2.CostModel.SerializedCostModel.Length)
            {
                // Console.WriteLine("CostModel.SerializedCostModel.Length");
                return false;
            }

            // Console.WriteLine(productModel1.CostModel.SerializedCostModel.Length+" "+productModel2.CostModel.SerializedCostModel.Length);

            if (productModel1.ProductSizeEnum != productModel2.ProductSizeEnum)
            {
                // Console.WriteLine("ProductSizeEnum");
                return false;
            }

            // Console.WriteLine(productModel1.ProductSizeEnum+" "+productModel2.ProductSizeEnum);

            if (productModel1.IsDisabled != productModel2.IsDisabled)
            {
                // Console.WriteLine("IsDisabled");
                return false;
            }

            // Console.WriteLine(productModel1.IsDisabled+" "+productModel2.IsDisabled);

            if (productModel1.ProductMark != productModel2.ProductMark)
            {
                // Console.WriteLine("ProductMark");
                return false;
            }

            // Console.WriteLine(productModel1.SerializedModel.Length+" "+productModel2.SerializedModel.Length);
            if (productModel1.ProductMark?.ProductMarkTypeEnum != productModel2.ProductMark?.ProductMarkTypeEnum)
            {
                // Console.WriteLine("ProductMark.ProductMarkTypeEnum");
                return false;
            }

            // Console.WriteLine(productModel1.ProductMark?.ProductMarkTypeEnum+" "+productModel2.ProductMark?.ProductMarkTypeEnum);

            if (productModel1.ProductMark?.SerializedProductMark.Length != productModel2.ProductMark?.SerializedProductMark.Length)
            {
                // Console.WriteLine("ProductMark.SerializedProductMark.Length");
                return false;
            }

            // Console.WriteLine(productModel1.ProductMark?.SerializedProductMark.Length+" "+productModel2.ProductMark?.SerializedProductMark.Length);

            if (productModel1.PreviewImagePath != productModel2.PreviewImagePath)
            {
                // Console.WriteLine("PreviewImagePath");
                return false;
            }

            // Console.WriteLine(productModel1.PreviewImagePath+" "+productModel2.PreviewImagePath);


            // Console.WriteLine("Проверка прошла успешно");
            return true;
        }
    }
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
            
            var productModelFromClient = ZeroFormatterSerializer
                .Deserialize<ProductModel>(serializedProductModelFromClient);
            // bool isEqual = new ProductChecker().IsEqual(productModelFromClient, productModelFromDb);
            
            if (!serializedProductModelFromClient.SequenceEqual(serializedProductModelFromDb))
            {
                Console.WriteLine(serializedProductModelFromClient.Length.ToString());
                Console.WriteLine(serializedProductModelFromDb.Length.ToString());
                throw new Exception("Модели продуктов не совпадают");
            }

            //создать транзакцию по модели продукта
            Transaction transaction = shopTransactionFactory.Create(productModelFromDb, account.Id);
            
            //todo проверить транзакцию на адекватность
            
            //записать транзакцию
            await dbContext.Transactions.AddAsync(transaction);
            
            
            //перезаписать модель продукта
            productModelFromDb.IsDisabled = true;
            shopModelDb.SerializedModel = ZeroFormatterSerializer.Serialize(shopModel);
            
            await dbContext.SaveChangesAsync();
        }
    }
}