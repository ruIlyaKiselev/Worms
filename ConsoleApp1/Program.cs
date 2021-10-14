using System;
using ConsoleApp1.Generators;
using ConsoleApp1.Logging;
using ConsoleApp1.WormsLogic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Host = Microsoft.Extensions.Hosting.Host;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // GameController gameController = new GameController();
            // gameController.GameProcess();
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddHostedService<GameControllerService>();
                    services.AddScoped<IFoodGenerator, FoodGenerator>();
                    services.AddScoped<INameGenerator, RandomNameGenerator>(_ => new RandomNameGenerator(new Random()));
                    services.AddScoped<IWormLogic, OptionalLogic>();
                    services.AddScoped<ILogger, Logger>();
                });
        }
    }
}