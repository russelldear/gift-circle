using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace GiftCircle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                    {
                        webBuilder
                            .UseUrls("http://0.0.0.0:5005")
                            .UseStartup<Startup>();
                    }
                    else
                    {
                        webBuilder
                            .UseUrls("http://0.0.0.0:5005")
                            .UseStartup<Startup>();
                    }
                });
    }
}
