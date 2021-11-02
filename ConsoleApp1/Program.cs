using System;
using ConsoleApp1.Database;
using ConsoleApp1.Generators;
using ConsoleApp1.Logging;
using ConsoleApp1.Repository;
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
                    PostgresDatabase postgresDatabase = new PostgresDatabase();
                    Console.WriteLine(postgresDatabase.GetVersion() + "))))))");
                    bool databaseExists = postgresDatabase.CheckDatabaseExists(PostgresContract.DBname);

                    if (!databaseExists)
                    {
                        postgresDatabase.CreateDatabase(PostgresContract.DBname);
                    }
            
                    databaseExists = postgresDatabase.CheckDatabaseExists(PostgresContract.DBname);
            
                    if (databaseExists)
                    {
                        postgresDatabase.ConnectToDatabase(PostgresContract.Host, PostgresContract.User,
                            PostgresContract.Password, PostgresContract.DBname);

                        if (!postgresDatabase.CheckTableExists(PostgresContract.TableName))
                        {
                            postgresDatabase.CreateTable();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cannot connect to database");
                    }
                    
                    services.AddHostedService<GameControllerService>();
                    services.AddScoped<IFoodGenerator, FoodGenerator>();
                    services.AddScoped<INameGenerator, RandomNameGenerator>(_ => new RandomNameGenerator(new Random()));
                    services.AddScoped<IWormLogic, OptionalLogic>();
                    services.AddScoped<ILogger, Logger>();
                    services.AddScoped<IRepository, RepositoryImpl>(_ => new RepositoryImpl(new PostgresDatabase()));
                });
        }
    }
}