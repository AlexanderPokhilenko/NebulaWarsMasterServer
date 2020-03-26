using System;
using System.Threading.Tasks;

namespace AmoebaGameMatcherServer.Services
{
    /// <summary>
    /// Нужен для проверки accessToken-а.
    /// </summary>
    public class IpAppProductsService
    {
        private readonly CustomGoogleApiAccessTokenService accessTokenService;

        public IpAppProductsService(CustomGoogleApiAccessTokenService accessTokenService)
        {
            this.accessTokenService = accessTokenService;
        }
        
        /// <summary>
        /// Выводт в консоль данные, которые могут быть получены из google api только после правильной авторизации.
        /// </summary>
        /// <returns></returns>
        public async Task GetInAppProducts()
        {
            string accessToken = accessTokenService.GetAccessToken();
            string responseContent = await GooglePurchasesApiWrapper.InAppProducts(accessToken);
            Console.WriteLine(responseContent);
        }
    }
}