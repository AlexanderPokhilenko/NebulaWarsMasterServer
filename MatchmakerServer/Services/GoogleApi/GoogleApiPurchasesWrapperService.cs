using System;
using System.Net.Http;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.GoogleApi.AccessTokenUtils;
using AmoebaGameMatcherServer.Services.GoogleApi.UrlFactories;
using JetBrains.Annotations;

namespace AmoebaGameMatcherServer.Services.GoogleApi
{
    /// <summary>
    /// Запрашивает ответ от Google для проверки того, что sku и token настоящие
    /// </summary>
    public class GoogleApiPurchasesWrapperService
    {
        private readonly CustomGoogleApiAccessTokenService accessTokenService;
        private readonly PurchaseValidateUrlFactory purchaseValidateUrlFactory;

        public GoogleApiPurchasesWrapperService(CustomGoogleApiAccessTokenService accessTokenService,
            PurchaseValidateUrlFactory purchaseValidateUrlFactory)
        {
            this.accessTokenService = accessTokenService;
            this.purchaseValidateUrlFactory = purchaseValidateUrlFactory;
        }
        
        public async Task<string> ValidateAsync([NotNull] string sku, [NotNull] string token)
        {
            string accessToken = accessTokenService.GetAccessToken();
            string url = purchaseValidateUrlFactory.Create(sku, token, accessToken);
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage result = await httpClient.GetAsync(url);
            
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