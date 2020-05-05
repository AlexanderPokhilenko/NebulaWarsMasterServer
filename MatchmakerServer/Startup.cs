using AmoebaGameMatcherServer.Services;
using AmoebaGameMatcherServer.Services.GoogleApi;
using AmoebaGameMatcherServer.Services.MatchCreationInitiation;
using AmoebaGameMatcherServer.Services.Queues;
using DataLayer;
using Google.Apis.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.AddFeature(new GoogleApiFeature());
            services.AddFeature(new DatabaseFeature());
            services.AddFeature(new LobbyInitializeFeature());
            services.AddFeature(new PlayerQueueingFeature());
            services.AddFeature(new MatchCreationInitiationFeature());
            services.AddFeature(new MatchCreationFeature());
            services.AddFeature(new MatchFinishingFeature());
            services.AddFeature(new GameServerNegotiationFeature());
            services.AddFeature(new LootboxFeature());
            services.AddFeature(new PurchasingFeature());

            //Общие очереди игроков
            services.AddSingleton<BattleRoyaleQueueSingletonService>();
            services.AddSingleton<BattleRoyaleUnfinishedMatchesSingletonService>();
            //Общие очереди игроков
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            MatchCreationInitiatorSingletonService matchCreationInitiator, 
            CustomGoogleApiAccessTokenService googleApiAccessTokenManagerService,
            ApplicationDbContext dbContext)
        {
            matchCreationInitiator.StartThread();
            googleApiAccessTokenManagerService.Initialize().Wait();

            //Заполнение данными
            new DataSeeder().TrySeed(dbContext);

            app.UseMvc();
        }
    }
}