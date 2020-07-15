using AmoebaGameMatcherServer.Controllers.ProfileServer.Lobby;
using AmoebaGameMatcherServer.Experimental;
using AmoebaGameMatcherServer.Services.Experimental;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer.Features
{
    public class LobbyInitializeFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<LobbyModelFacadeService>();
            serviceCollection.AddTransient<WarshipRatingScale>();
            serviceCollection.AddTransient<DefaultAccountFactoryService>();
            serviceCollection.AddTransient<AccountFacadeService>();
            serviceCollection.AddTransient<AccountDbReaderService>();
            serviceCollection.AddTransient<AccountRegistrationService>();
            serviceCollection.AddTransient<NotShownRewardsReaderService>();
            serviceCollection.AddTransient<DbWarshipsStatisticsReader>();
            serviceCollection.AddTransient<WarshipsCharacteristicsService>();
            serviceCollection.AddTransient<AccountMapperService>();
            serviceCollection.AddTransient<WarshipPowerScaleModelStorage>();
            serviceCollection.AddTransient<WarshipImprovementCostChecker>();
            serviceCollection.AddTransient<SkinsDbReaderService>();
            serviceCollection.AddTransient<DbAccountWarshipReaderService>();
            serviceCollection.AddTransient<StubUsernameDbWriterService>();
            serviceCollection.AddTransient<AccountResourcesDbReader>();
        }
    }
}