using System;
using System.Net.Http;
using System.Threading.Tasks;
using DataLayer;

namespace AmoebaGameMatcherServer.Services
{
    public static class GooglePurchasesApiWrapper
    {
        public static async Task<string> Get(string sku, string token, string accessToken)
        {
            HttpClient httpClient = new HttpClient();
            string url = CreateUrlForPurchaseCheck(sku, token, accessToken);
            Console.WriteLine($"{nameof(url)} {url}");
            var result = await httpClient.GetAsync(url);
            
            string content = await result.Content.ReadAsStringAsync();
            
            Console.WriteLine(result.StatusCode);
            Console.WriteLine(content);

            if (result.IsSuccessStatusCode)
            {
                return content;
            }
            else
            {
                Console.WriteLine($"{nameof(result.StatusCode)} {result.StatusCode}");
                return content;
            }
        }
        
        public static async Task<string> InAppProducts(string accessToken)
        {
            HttpClient httpClient = new HttpClient();
            string url = CreateUrlInAppProducts(accessToken);
            var result = await httpClient.GetAsync(url);
            
            string content = await result.Content.ReadAsStringAsync();
            
            Console.WriteLine(result.StatusCode);
            Console.WriteLine(content);

            if (result.IsSuccessStatusCode)
            {
                return content;
            }
            else
            {
                Console.WriteLine($"{nameof(result.StatusCode)} {result.StatusCode}");
                return content;
            }
        }
        
        private static string CreateUrlForPurchaseCheck(string sku, string token, string accessToken)
        {
            string result =
                $"https://www.googleapis.com/androidpublisher/v3/applications/{GoogleApiGlobals.PackageName}/purchases/products/{sku}/tokens/{token}?access_token="+accessToken;
            return result;
        }
        
        private static string CreateUrlInAppProducts(string accessToken)
        {
            string result =
                $"https://www.googleapis.com/androidpublisher/v3/applications/{GoogleApiGlobals.PackageName}/inappproducts?access_token={accessToken}";
            return result;
        }
    }
}