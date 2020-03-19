using System;
using System.Threading.Tasks;

namespace AmoebaGameMatcherServer.Services
{
    public class IpAppProductsService
    {
        private readonly CustomGoogleApiAccessTokenService accessTokenService;

        public IpAppProductsService(CustomGoogleApiAccessTokenService accessTokenService)
        {
            this.accessTokenService = accessTokenService;
        }
        
        public async Task GetInAppProducts()
        {
            string accessToken = accessTokenService.GetAccessToken();
            string responseContent = await GooglePurchasesApiWrapper.InAppProducts(accessToken);
            Console.WriteLine(responseContent);
        }
    }
}