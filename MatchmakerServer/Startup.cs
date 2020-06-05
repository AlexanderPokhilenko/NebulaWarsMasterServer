using System;
using AmoebaGameMatcherServer.Services.GoogleApi;
using AmoebaGameMatcherServer.Services.MatchCreationInitiation;
using AmoebaGameMatcherServer.Services.Queues;
using AutoMapper;
using Dapper;
using DataLayer;
using DataLayer.Tables;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NetworkLibrary.NetworkLibrary.Http;
using Npgsql;

namespace AmoebaGameMatcherServer
{
    public class MappingProfile : Profile 
    {
        public MappingProfile() {
            // Add as many of these lines as you need to map your objects
            CreateMap<Account, AccountDto>();
            CreateMap<AccountDto, Account>();
        }
    }
    
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

            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            
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
            
            //Заполнение данными
            new DataSeeder().TrySeed(dbContext);

            app.UseMvc();
        }
    }
}