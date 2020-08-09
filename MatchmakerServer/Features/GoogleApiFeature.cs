using AmoebaGameMatcherServer.Services.GoogleApi;
using AmoebaGameMatcherServer.Services.GoogleApi.AccessTokenUtils;
using AmoebaGameMatcherServer.Services.GoogleApi.UrlFactories;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer.Features
{
    public class GoogleApiFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<PurchaseAcknowledgeUrlFactory>();
            serviceCollection.AddTransient<PurchaseValidateUrlFactory>();
            serviceCollection.AddTransient<AllProductsUrlFactory>();
            
            serviceCollection.AddTransient<GoogleApiProfileStorageService>();
            serviceCollection.AddTransient<PackageNameStorageService>();
            
            serviceCollection.AddTransient<GoogleApiPurchasesWrapperService>();
            serviceCollection.AddTransient<GoogleApiPurchaseAcknowledgeService>();
            serviceCollection.AddTransient<PurchaseRegistrationService>();
            serviceCollection.AddSingleton<CustomGoogleApiAccessTokenService>();
        }
    }
}