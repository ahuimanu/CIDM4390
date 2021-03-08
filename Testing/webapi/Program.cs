using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using webapi.Data;
using repository;

namespace webapi
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //create service host
            var host = CreateHostBuilder(args).Build();

            //load stations - calls seeding routine using static methods in METARStation
            Loader(host);

            //continue
            host.Run();
        }

        public static void Loader(IHost host) {
            using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<WebApiDbContext>();
                LoadNOAAStationsData.Initialize(services);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
