using AmoebaGameMatcherServer.Services.GoogleApi.AccessTokenUtils;

namespace AmoebaGameMatcherServer.Services.GoogleApi.UrlFactories
{
    public class PackageNameStorageService
    {
        private readonly string packageName;

        public PackageNameStorageService(GoogleApiProfileStorageService profileStorageService)
        {
            packageName = profileStorageService.GetCurrentProfile().PackageName;
        }
        public string GetPackageName()
        {
            return packageName;
        }
    }
}