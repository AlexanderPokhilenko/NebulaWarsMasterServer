using DataLayer;

namespace AmoebaGameMatcherServer.Services.GoogleApi
{
    public class PurchaseAcknowledgeUrlFactory
    {
        public string Create(string productId, string token, string accessToken)
        {
            return
                $"https://www.googleapis.com/androidpublisher/v3/applications/{GoogleApiGlobals.PackageName}/purchases/products/productId/tokens/{token}:acknowledge?access_token={accessToken}";
        }
    }
}