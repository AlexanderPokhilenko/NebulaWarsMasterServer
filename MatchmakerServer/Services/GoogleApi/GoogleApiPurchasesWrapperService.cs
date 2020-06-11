using System;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace AmoebaGameMatcherServer.Services.GoogleApi
{
    /// <summary>
    /// Уведомляет google о том, что покупка была нормально записана в БД
    /// </summary>
    public class GoogleApiPurchaseAcknowledgeService
    {
        private readonly PurchaseAcknowledgeUrlFactory factory;
        private readonly CustomGoogleApiAccessTokenService accessTokenService;
        
        public GoogleApiPurchaseAcknowledgeService(CustomGoogleApiAccessTokenService accessTokenService)
        {
            this.accessTokenService = accessTokenService;
            factory = new PurchaseAcknowledgeUrlFactory();
        }
        
        public async Task Acknowledge(string productId, string token)
        {
            string accessToken = accessTokenService.GetAccessToken();
            string url = factory.Create(productId, token, accessToken);
            HttpClient httpClient = new HttpClient();
            
            //TODO возможно нужно добавить developer payload
            var result = await httpClient.GetAsync(url);
            
            if (result.IsSuccessStatusCode)
            {
                string content = await result.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
            else
            {
                throw new Exception("Не удалось уведомить о регистрации покупки.");
            }
        }
    }

    public class GoogleApiPurchasesWrapperService
    {
        private readonly CustomGoogleApiAccessTokenService accessTokenService;
        private readonly ProductValidateUrlFactory productValidateUrlFactory;

        public GoogleApiPurchasesWrapperService(CustomGoogleApiAccessTokenService accessTokenService)
        {
            this.accessTokenService = accessTokenService;
            productValidateUrlFactory = new ProductValidateUrlFactory();
        }
        
        public async Task<string> Validate([NotNull] string sku, [NotNull] string token)
        {
            string accessToken = accessTokenService.GetAccessToken();
            string url = productValidateUrlFactory.Create(sku, token, accessToken);
            HttpClient httpClient = new HttpClient();
            var result = await httpClient.GetAsync(url);
            
            if (result.IsSuccessStatusCode)
            {
                string content = await result.Content.ReadAsStringAsync();
                return content;
            }
            else
            {
                Console.WriteLine($"{nameof(result.StatusCode)} {result.StatusCode}");
                return null;
            }
        }
    }
}