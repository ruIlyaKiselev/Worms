using System;
using ConsoleApp1.CoreGame;
using ConsoleApp1.CoreGame.Domain;
using ConsoleApp1.Database;
using ConsoleApp1.Generators;
using ConsoleApp1.Logging;
using ConsoleApp1.Network;
using ConsoleApp1.Repository;
using ConsoleApp1.WormsLogic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;

namespace ConsoleApp1
{
    class Program
    {
        /// <summary>
        ///     Точка входа.
        ///     Создает службу и запускает процесс модуляции.
        ///
        ///     Внимание!
        ///
        ///     В аргументы программы следует передать строку-имя поведения мира, который будет браться из БД. 
        ///     Если в БД нет поведения с таким именем, оно сгенерируется автоматически, ничего делать не нужно.
        /// 
        ///     Для корректной работы перед запуском этого модуля запустите модуль NetworkModule.
        ///     Иначе программа будет работать долго, так как будет принимать решение о действии червя локально,
        ///     по таймауту API запроса.
        ///
        ///     Убедитесь, что установлена PostgreSQL и в конфиг-файле AppModule/Database/PostgresContract
        ///     введены актуальные данные, аккаунт должен быть с правами на запись.
        /// </summary>
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    InitDatabase(args[0]);
                    
                    services.AddHostedService<GameControllerService>();
                    services.AddScoped<IFoodGenerator, FoodGenerator>();
                    services.AddScoped<INameGenerator, RandomNameGenerator>(_ => new RandomNameGenerator(new Random()));
                    services.AddScoped<IWormLogic, OptionalLogic>();
                    services.AddScoped<ILogger, Logger>();
                    services.AddScoped<IRepository, RepositoryImpl>(_ => new RepositoryImpl(
                            new PostgresDatabaseORM(),
                            NetworkServiceFactory.GetNetworkService(
                                NetworkContract.BASE_URL,
                                NetworkContract.HOST,
                                NetworkContract.PORT
                            )
                        )
                    );
                    services.AddSingleton(new IntegrationService(args[0]));
                });
        }

        private static void InitDatabase(string worldBehaviorName)
        {
            PostgresDatabaseORM database = new PostgresDatabaseORM();
            if (!database.CheckWorldBehaviorExists(worldBehaviorName))
            {
                WorldBehavior worldBehavior = new WorldBehavior(worldBehaviorName);
                database.SaveWorldBehavior(worldBehavior.ToEntity());
            }
        }
    }
}