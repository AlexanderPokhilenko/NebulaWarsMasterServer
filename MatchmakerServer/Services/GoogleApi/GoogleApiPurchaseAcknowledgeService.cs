using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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
        
        public async Task Acknowledge(string productId, string token, string developerPayload)
        {
            string accessToken = accessTokenService.GetAccessToken();
            string url = factory.Create(productId, token, accessToken);
            HttpClient httpClient = new HttpClient();

            
            JObject jObject = new JObject();
            jObject.Add("developerPayload", developerPayload);
            string developerPayloadJson = jObject.ToString();
            HttpContent httpContent = new StringContent(developerPayloadJson);
            var result = await httpClient.PostAsync(url, httpContent);
            
            if (result.IsSuccessStatusCode)
            {
                string content = await result.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine(result.StatusCode);
                throw new Exception("Не удалось уведомить о регистрации покупки.");
            }
        }
    }
}