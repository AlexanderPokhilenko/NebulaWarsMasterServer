using AmoebaGameMatcherServer.Services.GoogleApi;
using AmoebaGameMatcherServer.Services.GoogleApi.AccessTokenUtils;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer.Features
{
    public class GoogleApiFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<PurchasesValidatorService>();
            serviceCollection.AddTransient<GoogleApiPurchasesWrapperService>();
            serviceCollection.AddTransient<GoogleApiPurchaseAcknowledgeService>();
            serviceCollection.AddTransient<PurchaseRegistrationService>();
            serviceCollection.AddSingleton<CustomGoogleApiAccessTokenService>();
            // serviceCollection.AddSingleton<IpAppProductsService>();
        }
    }
}