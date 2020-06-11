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

            string responseContent = await GooglePurchasesApiWrapper.Get(sku, token, accessToken);

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
            dbContext.Purchases.Add(new TestPurchase
            {
                Data = responseContent
            });
            dbContext.SaveChanges();
        }
    }
}