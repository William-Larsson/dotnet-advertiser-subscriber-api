using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using advertising.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace advertising
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Load in the environment variabels using the DotEnv helper class. 
            var root = Directory.GetCurrentDirectory();
            var envPath = Path.Combine(root, ".env");
            DotEnv.Load(envPath);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
