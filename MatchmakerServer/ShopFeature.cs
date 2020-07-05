using AmoebaGameMatcherServer.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer
{
    public class ShopFeature : ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ShopFactoryService>();
            serviceCollection.AddTransient<DailyDealsSectionFactory>();
            serviceCollection.AddTransient<PrizeFactoryService>();
            serviceCollection.AddTransient<WarshipPowerPointsProductsFactoryService>();
        }
    }
}