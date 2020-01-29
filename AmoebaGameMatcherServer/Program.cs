using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

//TODO добавить возможность опроса гейм сервера для сбора актуальных данных о игровых сессиях

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