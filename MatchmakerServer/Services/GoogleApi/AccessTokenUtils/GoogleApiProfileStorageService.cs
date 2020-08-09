using DataLayer;

namespace AmoebaGameMatcherServer.Services.GoogleApi.AccessTokenUtils
{
    public class GoogleApiProfileStorageService
    {
        public GoogleApiProfile GetCurrentProfile()
        {
            return new GoogleApiProfileNewest();
        }
    }
}