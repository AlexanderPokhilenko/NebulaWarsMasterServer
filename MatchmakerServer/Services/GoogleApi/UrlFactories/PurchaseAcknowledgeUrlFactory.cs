using DataLayer;

namespace AmoebaGameMatcherServer.Services.GoogleApi.UrlFactories
{
    public class PurchaseAcknowledgeUrlFactory
    {
        private readonly string packageName;

        public PurchaseAcknowledgeUrlFactory(PackageNameStorageService packageNameStorageService)
        {
            packageName = packageNameStorageService.GetPackageName();
        }
        
        public string Create(string productId, string token, string accessToken)
        {
            return $"https://www.googleapis.com/androidpublisher/v3/applications/{packageName}/purchases/products/{productId}/tokens/{token}:acknowledge?access_token={accessToken}";
        }
    }
}