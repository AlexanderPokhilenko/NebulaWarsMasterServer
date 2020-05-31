using System;
using AmoebaGameMatcherServer.NetworkLibrary;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using ZeroFormatter;

namespace AmoebaGameMatcherServer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            
            
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}