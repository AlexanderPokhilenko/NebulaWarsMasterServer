using AmoebaGameMatcherServer.Controllers;
using AmoebaGameMatcherServer.Services;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer
{
    public class LobbyInitializeFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<LobbyModelFacadeService>();
            serviceCollection.AddTransient<WarshipRatingScale>();
            // serviceCollection.AddTransient<WarshipPowerScaleModelStorage>();
            serviceCollection.AddTransient<AccountFacadeService>();
            serviceCollection.AddTransient<AccountDbReaderService>();
            serviceCollection.AddTransient<AccountRegistrationService>();
            serviceCollection.AddTransient<NotShownRewardService>();
        }
    }
}