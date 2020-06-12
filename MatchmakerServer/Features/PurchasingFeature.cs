using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer
{
    public class PurchasingFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<OrderConfirmationService>();
        }
    }
}