using DataLayer;

namespace AmoebaGameMatcherServer.Services.GoogleApi.UrlFactories
{
    public class AllProductsUrlFactory
    {
        private readonly string packageName;

        public AllProductsUrlFactory(PackageNameStorageService packageNameStorageService)
        {
            packageName = packageNameStorageService.GetPackageName();
        }
        
        public string Create(string accessToken)
        {
            string result = $"https://www.googleapis.com/androidpublisher/v3/applications/{packageName}/inappproducts?access_token={accessToken}";
            return result;
        }
    }
}