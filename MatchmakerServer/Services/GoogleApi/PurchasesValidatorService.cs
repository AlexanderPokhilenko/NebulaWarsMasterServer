using System;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using Newtonsoft.Json;

namespace AmoebaGameMatcherServer.Services.GoogleApi
{
    /// <summary>
    /// Отвечает за начисление предметов после совершения покупки в google play store
    /// </summary>
    public class PurchasesValidatorService
    {
        private readonly CustomGoogleApiAccessTokenService accessTokenService;
        private readonly ApplicationDbContext dbContext;
        
        public PurchasesValidatorService(CustomGoogleApiAccessTokenService accessTokenService, 
            ApplicationDbContext dbContext)
        {
            this.accessTokenService = accessTokenService;
            this.dbContext = dbContext;
        }

        public async Task Validate(string sku, string token)
        {
            Console.WriteLine($"{nameof(sku)} {sku} {nameof(token)} {token}");
            
            string accessToken = accessTokenService.GetAccessToken();
            Console.WriteLine($"{nameof(accessToken)} {accessToken}");

            string responseContentJson = await GooglePurchasesApiWrapper.Get(sku, token, accessToken);

            
            if (responseContentJson != null)
            {
                Console.WriteLine($"{nameof(responseContentJson)} {responseContentJson}");
                SaveResponseContentToDb(responseContentJson);
            }
            else
            {
                Console.WriteLine($"{nameof(responseContentJson)} was null");   
            }
        }

        private void SaveResponseContentToDb(string responseContentJson)
        {
            dynamic jsonObj = JsonConvert.DeserializeObject(responseContentJson);
            try
            {
                int acknowledgementState = (int) jsonObj["acknowledgementState"];
                int consumptionState = (int) jsonObj["consumptionState"];
                string developerPayload = jsonObj["developerPayload"];
                string kind = jsonObj["kind"];
                string orderId = jsonObj["orderId"];
                int purchaseState = (int) jsonObj["purchaseState"];
                long purchaseTimeMillis = (long) jsonObj["purchaseTimeMillis"];
                int purchaseType = (int) jsonObj["purchaseType"];
                
                
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(purchaseTimeMillis);
                dbContext.Purchases.Add(new TestPurchase
                {
                    Data = responseContentJson,
                    DateTime = dateTimeOffset.DateTime,
                    AcknowledgementState = acknowledgementState,
                    ConsumptionState = consumptionState,
                    DeveloperPayload = developerPayload,
                    Kind = kind,
                    OrderId = orderId,
                    PurchaseState = purchaseState,
                    PurchaseTimeMillis = purchaseTimeMillis,
                    PurchaseType = purchaseType
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