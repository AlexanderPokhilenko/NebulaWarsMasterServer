using AmoebaGameMatcherServer.Controllers;
using Code.Scenes.LobbyScene.Scripts;
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
            serviceCollection.AddTransient<ShopModelDbReader>();
            serviceCollection.AddTransient<ShopFactoryService>();
            serviceCollection.AddTransient<ShopWriterService>();
            serviceCollection.AddTransient<IncrementFactoriesService>();
            serviceCollection.AddTransient<DecrementFactoriesService>();
            serviceCollection.AddTransient<HardCurrencySectionFactory>();
        }
    }
}