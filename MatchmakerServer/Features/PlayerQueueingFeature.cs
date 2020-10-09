using AmoebaGameMatcherServer.Services.PlayerQueueing;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer.Features
{
    public class PlayerQueueingFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMatchmakerFacadeService, MatchmakerFacadeService>();
            serviceCollection.AddTransient<IQueueExtenderService, QueueExtenderService>();
            serviceCollection.AddTransient<MatchRoutingDataService>();
            serviceCollection.AddTransient<GameServersRoutingDataService>();
        }
    }
}