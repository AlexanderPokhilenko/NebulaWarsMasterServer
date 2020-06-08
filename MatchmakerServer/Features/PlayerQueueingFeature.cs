using AmoebaGameMatcherServer.Services.PlayerQueueing;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer
{
    public class PlayerQueueingFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<MatchmakerFacadeService>();
            serviceCollection.AddTransient<QueueExtenderService>();
            serviceCollection.AddTransient<MatchRoutingDataService>();
            serviceCollection.AddTransient<GameServersRoutingDataService>();
        }
    }
}