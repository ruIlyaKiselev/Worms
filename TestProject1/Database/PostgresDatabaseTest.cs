using System;
using ConsoleApp1.Database;
using NUnit.Framework;

namespace TestProject1.Database
{
    public class PostgresDatabaseTest
    {
        [Test]
        public void BuddingToOtherWormPosition()
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

            ConsoleApp1.WorldBehavior worldBehavior1 = new ConsoleApp1.WorldBehavior("test1");
            ConsoleApp1.WorldBehavior worldBehavior2 = new ConsoleApp1.WorldBehavior("test2");
            ConsoleApp1.WorldBehavior worldBehavior3 = new ConsoleApp1.WorldBehavior("test3");
            postgresDatabase.SaveWorldBehavior(worldBehavior1.Name, Converters.convertListToJson(worldBehavior1.FoodCoords));
            postgresDatabase.SaveWorldBehavior(worldBehavior2.Name, Converters.convertListToJson(worldBehavior2.FoodCoords));
            postgresDatabase.SaveWorldBehavior(worldBehavior3.Name, Converters.convertListToJson(worldBehavior3.FoodCoords));
            
            var restoredWorld = postgresDatabase.GetWorldBehaviorByName("test1");
            
            // postgresDatabase.UpdateWorldBehaviorByName("test3", restoredWorld[1]);
            
            if (restoredWorld == null)
            {
                Console.WriteLine("Not found!");
            }
            else
            {
                foreach (var s in restoredWorld)
                {
                    Console.WriteLine(s);
                }
            }
        }
    }
}