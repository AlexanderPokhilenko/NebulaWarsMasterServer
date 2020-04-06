using AmoebaGameMatcherServer.Services;
using AmoebaGameMatcherServer.Services.MatchCreationInitiation;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer
{
    public class MatchCreationInitiationFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<MatchCreationInitiatorSingletonService>();
            serviceCollection.AddTransient<IPlayerTimeoutManager, PlayerTimeoutManagerService>();
        }
    }
}