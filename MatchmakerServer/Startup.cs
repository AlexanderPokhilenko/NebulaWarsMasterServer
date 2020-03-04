using System;
using AmoebaGameMatcherServer.Services;
using DataLayer;
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

            
            string connectionString = new DbConfig().GetConnectionString();

            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(connectionString))
                .BuildServiceProvider();
            
            services.AddSingleton<GameMatcherDataService>();
            services.AddSingleton<GameMatcherService>();
            services.AddSingleton<GameMatcherForceRoomCreator>();
            services.AddSingleton<GameServerNegotiatorService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, GameMatcherForceRoomCreator forceRoomCreator, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            forceRoomCreator.StartPeriodicCreationInAnotherThread();
            // app.UseHttpsRedirection();
            app.UseMvc();

            int count = dbContext.Accounts.CountAsync().Result;
            Console.WriteLine("account count = "+count);
        }
    }
}