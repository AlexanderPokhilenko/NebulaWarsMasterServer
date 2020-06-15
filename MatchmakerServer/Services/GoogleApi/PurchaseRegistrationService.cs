using System;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
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

        public async Task TryEnterPurchaseIntoDbAsync(string googleResponseJson, string sku, string token, int accountId)
        {
            Console.WriteLine(nameof(TryEnterPurchaseIntoDbAsync)+" "+sku+" "+token+" "+accountId);
            dynamic jsonObj = JsonConvert.DeserializeObject(googleResponseJson);
            try
            {
                int acknowledgementState = (int) jsonObj["acknowledgementState"];
                int consumptionState = (int) jsonObj["consumptionState"];
                string developerPayload = jsonObj["developerPayload"];
                string kind = jsonObj["kind"];
                string orderId = jsonObj["orderId"];
                int purchaseState = (int) jsonObj["purchaseState"];
                long purchaseTimeMillis = (long) jsonObj["purchaseTimeMillis"];
                int? purchaseType = (int?) jsonObj["purchaseType"];
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(purchaseTimeMillis);
                
                //TODO проверить, что такой засиси нет в БД
                TestPurchase purchase = await dbContext.Purchases
                    .Where(purchase1 => purchase1.OrderId == orderId)
                    .SingleOrDefaultAsync();

                if (purchase == null)
                {
                    dbContext.Purchases.Add(new TestPurchase
                    {
                        Data = googleResponseJson,
                        DateTime = dateTimeOffset.DateTime,
                        AcknowledgementState = acknowledgementState,
                        ConsumptionState = consumptionState,
                        DeveloperPayload = developerPayload,
                        Kind = kind,
                        OrderId = orderId,
                        PurchaseState = purchaseState,
                        PurchaseTimeMillis = purchaseTimeMillis,
                        PurchaseType = purchaseType,
                        Sku = sku,
                        Token = token,
                        IsConfirmed = false,
                        AccountId = accountId
                    });
                    dbContext.SaveChanges();
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