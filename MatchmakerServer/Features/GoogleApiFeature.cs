using AmoebaGameMatcherServer.Services.GoogleApi;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer
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