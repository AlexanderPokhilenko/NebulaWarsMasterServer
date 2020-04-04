using AmoebaGameMatcherServer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer
{
    public class GoogleApiFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<PurchasesValidatorService>();
            serviceCollection.AddSingleton<CustomGoogleApiAccessTokenService>();
            serviceCollection.AddSingleton<IpAppProductsService>();
        }
    }
}