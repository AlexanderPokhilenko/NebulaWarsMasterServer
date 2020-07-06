using AmoebaGameMatcherServer.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer
{
    public class ShopFeature : ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ShopService>();
            serviceCollection.AddTransient<DailyDealsSectionFactory>();
            serviceCollection.AddTransient<PrizeFactoryService>();
            serviceCollection.AddTransient<WarshipPowerPointsProductsFactoryService>();
            serviceCollection.AddTransient<SellerService>();
            serviceCollection.AddTransient<ShopTransactionFactory>();
        }
    }
}