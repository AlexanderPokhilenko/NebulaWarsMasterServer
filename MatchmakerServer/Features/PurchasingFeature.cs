using AmoebaGameMatcherServer.Services.GoogleApi;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer.Features
{
    public class PurchasingFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<OrderConfirmationService>();
        }
    }
}