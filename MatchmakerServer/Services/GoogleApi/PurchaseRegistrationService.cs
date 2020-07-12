using System;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AmoebaGameMatcherServer.Services.GoogleApi
{
    /// <summary>
    /// Вносит информацию про покупку за реальные деньги в БД
    /// </summary>
    public class PurchaseRegistrationService
    {
        private readonly ApplicationDbContext dbContext;

        public PurchaseRegistrationService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task TryEnterPurchaseIntoDbAsync([NotNull]string googleResponseJson, [NotNull]string sku,
            [NotNull] string token, int accountId)
        {
            Console.WriteLine(nameof(TryEnterPurchaseIntoDbAsync)+" "+sku+" "+token+" "+accountId);
            try
            {
                dynamic jsonObj = JsonConvert.DeserializeObject(googleResponseJson);
                if (jsonObj == null)
                {
                    throw new Exception("Не удалось конвертировать ответ гугла в json");
                }
                int acknowledgementState = (int) jsonObj["acknowledgementState"];
                int consumptionState = (int) jsonObj["consumptionState"];
                string developerPayload = jsonObj["developerPayload"];
                string kind = jsonObj["kind"];
                string orderId = jsonObj["orderId"];
                int purchaseState = (int) jsonObj["purchaseState"];
                long purchaseTimeMillis = (long) jsonObj["purchaseTimeMillis"];
                int? purchaseType = (int?) jsonObj["purchaseType"];
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(purchaseTimeMillis);
                
                //проверить, что такой засиси нет в БД
                TestPurchase purchase = await dbContext.Purchases
                    .Where(purchase1 => purchase1.TransactionId == orderId)
                    .SingleOrDefaultAsync();
            
                if (purchase == null)
                {
                    await dbContext.Purchases.AddAsync(new TestPurchase
                    {
                        Data = googleResponseJson,
                        CreationDateTime = dateTimeOffset.DateTime,
                        AcknowledgementState = acknowledgementState,
                        ConsumptionState = consumptionState,
                        DeveloperPayload = developerPayload,
                        Kind = kind,
                        TransactionId = orderId,
                        PurchaseState = purchaseState,
                        PurchaseTimeMillis = purchaseTimeMillis,
                        PurchaseType = purchaseType,
                        Sku = sku,
                        Token = token,
                        IsConfirmed = false,
                        AccountId = accountId
                    });
                    await dbContext.SaveChangesAsync();
                    Console.WriteLine("Успешное сохраниение в БД");
                }
                else
                {
                    Console.WriteLine(purchase.Id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message+" "+e.StackTrace);
            }
        }
    }
}