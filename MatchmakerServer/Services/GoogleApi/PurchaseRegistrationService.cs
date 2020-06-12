using System;
using DataLayer;
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

        public void EnterPurchaseIntoDb(string googleResponseJson, string sku, string token, int accountId)
        {
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
                    IsPurchaseConfirmed = false,
                    AccountId = accountId
                });
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message+" "+e.StackTrace);
            }
        }

        
    }
}