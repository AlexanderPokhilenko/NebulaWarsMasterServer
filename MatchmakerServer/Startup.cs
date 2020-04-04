using AmoebaGameMatcherServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
            services.AddFeature(new MatchFinishingFeature());
            services.AddFeature(new MatchCreationFeature());

            //Общие очереди игроков
            services.AddSingleton<BattleRoyaleQueueSingletonService>();
            services.AddSingleton<BattleRoyaleUnfinishedMatchesSingletonService>();
            //Общие очереди игроков
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            MatchCreationInitiatorSingletonService matchCreationInitiator, 
            CustomGoogleApiAccessTokenService googleApiAccessTokenManagerService)
        {
            matchCreationInitiator.StartThread();
            googleApiAccessTokenManagerService.Initialize().Wait();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseMvc();
        }
    }
}