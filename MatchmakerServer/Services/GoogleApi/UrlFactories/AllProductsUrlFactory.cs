using DataLayer;

namespace AmoebaGameMatcherServer.Services.GoogleApi
{
    public class AllProductsUrlFactory
    {
        public string Create(string accessToken)
        {
            string result =
                $"https://www.googleapis.com/androidpublisher/v3/applications/{GoogleApiGlobals.PackageName}/inappproducts?access_token={accessToken}";
            return result;
        }
    }
}