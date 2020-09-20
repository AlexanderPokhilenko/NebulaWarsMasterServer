using DataLayer;

namespace AmoebaGameMatcherServer.Services.GoogleApi.UrlFactories
{
    public class PurchaseValidateUrlFactory
    {
        private readonly string packageName;

        public PurchaseValidateUrlFactory(PackageNameStorageService packageNameStorageService)
        {
            packageName = packageNameStorageService.GetPackageName();
        }
        
        public string Create(string sku, string token, string accessToken)
        {
            string result = $"https://www.googleapis.com/androidpublisher/v3/applications/{packageName}/purchases/products/{sku}/tokens/{token}/?access_token={accessToken}";
            return result;
        }
    }
}