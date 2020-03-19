using System;
using System.Linq;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services;
using DataLayer;
using DataLayer.Tables;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            // Configuration = configuration;
        }

        // public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            string connectionString = DbConfigIgnore.GetConnectionString();
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(connectionString))
                .BuildServiceProvider();

            services.AddTransient<PlayerLobbyInitializeService>();
            services.AddTransient<WarshipInfoHelper>();

            // services.AddSingleton<GooglePurchasesWrapperService>();
            
            services.AddSingleton<CustomGoogleApiAccessTokenService>();
            services.AddSingleton<PurchasesValidatorService>();
            services.AddSingleton<IpAppProductsService>();
            
            services.AddSingleton<GameMatcherDataService>();
            services.AddSingleton<GameMatcherService>();
            services.AddSingleton<GameMatcherForceRoomCreator>();
            services.AddSingleton<GameServerNegotiatorService>();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            GameMatcherForceRoomCreator forceRoomCreator, ApplicationDbContext dbContext, 
            CustomGoogleApiAccessTokenService googleApiAccessTokenManagerService)
        {
            forceRoomCreator.StartPeriodicCreationInAnotherThread();
            googleApiAccessTokenManagerService.Initialize().Wait();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}