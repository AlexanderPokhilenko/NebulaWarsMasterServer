using System;
using AmoebaGameMatcherServer.Services.GoogleApi;
using AmoebaGameMatcherServer.Services.MatchCreationInitiation;
using AmoebaGameMatcherServer.Services.Queues;
using Dapper;
using DataLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace AmoebaGameMatcherServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient<NpgsqlConnection>(provider =>
            {
                string connectionString = DbConfigIgnore.GetConnectionString();
                return new NpgsqlConnection(connectionString);
            });
            
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
            ApplicationDbContext dbContext, NpgsqlConnection npgsqlConnection)
        {
            matchCreationInitiator.StartThread();
            googleApiAccessTokenManagerService.Initialize().Wait();

            Console.WriteLine(npgsqlConnection.Query($@"select * from ""Accounts"" "));
            
            app.Use(async (context, next) =>
                    {
                        Console.WriteLine("Лог "+context.Request.Path);
                        
                        await next.Invoke();
                    });
            //Заполнение данными
            new DataSeeder().TrySeed(dbContext);

            app.UseMvc();
        }
    }
}