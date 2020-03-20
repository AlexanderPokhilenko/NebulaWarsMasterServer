using System;
using DataLayer;
using DataLayer.Tables;
using Newtonsoft.Json;

namespace AmoebaGameMatcherServer.Services
{
    public class PurchasesValidatorService
    {
        private readonly CustomGoogleApiAccessTokenService accessTokenService;

        public PurchasesValidatorService(CustomGoogleApiAccessTokenService accessTokenService)
        {
            this.accessTokenService = accessTokenService;
        }

        public void Validate(string sku, string token)
        {
            Console.WriteLine($"{nameof(sku)} {sku} {nameof(token)} {token}");
            
            string accessToken = accessTokenService.GetAccessToken();
            Console.WriteLine($"{nameof(accessToken)} {accessToken}");

            string responseContent = GooglePurchasesApiWrapper.Get(sku, token, accessToken).Result;

            if (responseContent != null)
            {
                Console.WriteLine($"{nameof(responseContent)} {responseContent}");
                SaveResponseContentToDb(responseContent);
            }
            else
            {
                Console.WriteLine($"{nameof(responseContent)} was null");   
            }
        }

        private void SaveResponseContentToDb(string responseContent)
        {
            dynamic responseObj = JsonConvert.DeserializeObject(responseContent);
            try
            {
                string kind = responseObj.kind;
                long purchaseTimeMillis = responseObj.purchaseTimeMillis;
                int purchaseState = responseObj.purchaseState;
                int consumptionState = responseObj.consumptionState;
                string developerPayload = responseObj.developerPayload;
                string orderId = responseObj.orderId;
                int purchaseType = responseObj.purchaseType;
                int acknowledgementState = responseObj.acknowledgementState;
            
                using (ApplicationDbContext dbContext = DbContextFactory.CreateDbContext())
                {
                    Purchase purchase = new Purchase
                    {
                        Json = responseContent,
                        Kind = kind,
                        PurchaseTimeMillis = purchaseTimeMillis,
                        PurchaseState = purchaseState,
                        ConsumptionState = consumptionState,
                        DeveloperPayload = developerPayload,
                        OrderId = orderId,
                        PurchaseType = purchaseType,
                        AcknowledgementState = acknowledgementState
                    };
                    dbContext.Purchases.Add(purchase);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}