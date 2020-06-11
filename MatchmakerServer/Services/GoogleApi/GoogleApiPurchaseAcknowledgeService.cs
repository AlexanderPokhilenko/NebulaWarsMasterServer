using System;
using System.Net.Http;
using System.Threading.Tasks;

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
}