using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AmoebaGameMatcherServer.Services.GoogleApi.AccessTokenUtils
{
    public static class CustomGoogleApiInitializer
    {
        public static async Task<string> GetAuthData(InitializeAccessTokenArg initAccessToken)
        {
            HttpClient httpClient = new HttpClient();
            Dictionary<string, string> requestData = new Dictionary<string, string>()
            {
                {"grant_type", "authorization_code"},
                {"code", initAccessToken.Code},
                {"client_id", initAccessToken.ClientId},
                {"client_secret", initAccessToken.ClientSecret},
                {"redirect_uri", initAccessToken.RedirectUri}
            };
            
            HttpContent  httpContent = new FormUrlEncodedContent(requestData);
            const string url ="https://accounts.google.com/o/oauth2/token";
            
            var responseMessage = await httpClient.PostAsync(url, httpContent);
            string responseContent = await responseMessage.Content.ReadAsStringAsync();
            
            Console.WriteLine($"{nameof(responseMessage.StatusCode)} {responseMessage.StatusCode}");
            Console.WriteLine($"{nameof(responseContent)} {responseContent}");
            
            if (responseMessage.IsSuccessStatusCode)
            {
                Console.WriteLine($"responseMessage is ok status");
                return responseContent;
            }

            throw new Exception("2 Не удалось получить токен.");
        }
    }
}