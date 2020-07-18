using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AmoebaGameMatcherServer.Services.GoogleApi.AccessTokenUtils
{
    public static class TokenManagerService
    {
        public static async Task<GoogleApiAuthData> InitializeAccessTokenAsync(InitializeAccessTokenArg initAccessToken)
        {
            string responseContent = await CustomGoogleApiInitializer.GetAuthData(initAccessToken);
            
            dynamic responseObj = JsonConvert.DeserializeObject(responseContent);
            
            // string tokenType = responseObj.token_type;
            int expiresIn = responseObj.expires_in;
            string accessToken = responseObj.access_token;
            string refreshToken = responseObj.refresh_token;

            GoogleApiAuthData result = new GoogleApiAuthData
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresInSec = expiresIn,
                AccessTokenCreationTime = DateTime.UtcNow
            };
            
            return result;
        }


        public static async Task<RefreshedData> UpdateAccessToken(AccessTokenUpdatingArg tokenUpdatingArg)
        {
            var responseContent = await CustomGoogleApiAccessTokenUpdater
                .RenewAccessToken(tokenUpdatingArg);
            
            dynamic responseObj = JsonConvert.DeserializeObject(responseContent);
            
            int expiresIn = responseObj.expires_in;
            string accessToken = responseObj.access_token;

            RefreshedData result = new RefreshedData
            {
                AccessToken = accessToken,
                ExpiresInSec = expiresIn
            };
            return result;
        }
    }
}