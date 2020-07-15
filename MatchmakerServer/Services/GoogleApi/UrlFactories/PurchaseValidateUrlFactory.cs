using DataLayer;

namespace AmoebaGameMatcherServer.Services.GoogleApi.UrlFactories
{
    public class PurchaseValidateUrlFactory
    {
        public string Create(string sku, string token, string accessToken)
        {
            string result =
                $"https://www.googleapis.com/androidpublisher/v3/applications/{GoogleApiGlobals.PackageName}/purchases/products/{sku}/tokens/{token}/?access_token={accessToken}";
            return result;
        }
    }
}