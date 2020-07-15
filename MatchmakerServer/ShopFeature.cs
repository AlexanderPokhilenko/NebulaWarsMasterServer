using AmoebaGameMatcherServer.Controllers;
using AmoebaGameMatcherServer.Features;
using AmoebaGameMatcherServer.Services.Shop.Sales;
using AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation;
using AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation.DecrementCreation;
using AmoebaGameMatcherServer.Services.Shop.Sales.TransactionCreation.IncrementCreation;
using AmoebaGameMatcherServer.Services.Shop.ShopModel;
using AmoebaGameMatcherServer.Services.Shop.ShopModel.DeleteMeShopSectionFactories;
using AmoebaGameMatcherServer.Services.Shop.ShopModel.ShopModelCreation;
using AmoebaGameMatcherServer.Services.Shop.ShopModel.ShopModelDbReading;
using AmoebaGameMatcherServer.Services.Shop.ShopModel.ShopModelDbWriting;
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