using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1;
using ConsoleApp1.Database;
using ConsoleApp1.Generators;
using ConsoleApp1.WormsLogic;
using NUnit.Framework;

namespace TestProject1.Database
{
    public class EntityFrameworkORMTest
    {
        [Test]
        public void SaveAndDeleteWorldBehavior()
        {
            PostgresDatabaseORM database = new PostgresDatabaseORM();
            
            ConsoleApp1.WorldBehavior worldBehavior1 = new ConsoleApp1.WorldBehavior("test1");
            ConsoleApp1.WorldBehavior worldBehavior2 = new ConsoleApp1.WorldBehavior("test2");
            ConsoleApp1.WorldBehavior worldBehavior3 = new ConsoleApp1.WorldBehavior("test3");
            
            bool worldBehavior1Exists = database.CheckWorldBehaviorExists(worldBehavior1.Name);
            bool worldBehavior2Exists = database.CheckWorldBehaviorExists(worldBehavior2.Name);
            bool worldBehavior3Exists = database.CheckWorldBehaviorExists(worldBehavior3.Name);

            if (worldBehavior1Exists)
            {
                Assert.Throws<InvalidOperationException>(() =>
                {
                    database.SaveWorldBehavior(worldBehavior1.ToDTO());
                });
                worldBehavior1 = database.GetWorldBehaviorByName(worldBehavior1.Name).ToDomain();
            }
            else
            {
                database.SaveWorldBehavior(worldBehavior1.ToDTO());
            }
            
            if (worldBehavior2Exists)
            {
                Assert.Throws<InvalidOperationException>(() =>
                {
                    database.SaveWorldBehavior(worldBehavior2.ToDTO());
                });
                worldBehavior2 = database.GetWorldBehaviorByName(worldBehavior2.Name).ToDomain();
            }
            else
            {
                database.SaveWorldBehavior(worldBehavior2.ToDTO());
            }
            
            if (worldBehavior3Exists)
            {
                Assert.Throws<InvalidOperationException>(() =>
                {
                    database.SaveWorldBehavior(worldBehavior3.ToDTO());
                });
                worldBehavior3 = database.GetWorldBehaviorByName(worldBehavior3.Name).ToDomain();
            }
            else
            {
                database.SaveWorldBehavior(worldBehavior3.ToDTO());
            }

            worldBehavior1Exists = database.CheckWorldBehaviorExists(worldBehavior1.Name);
            worldBehavior2Exists = database.CheckWorldBehaviorExists(worldBehavior2.Name);
            worldBehavior3Exists = database.CheckWorldBehaviorExists(worldBehavior3.Name);

            Assert.True(worldBehavior1Exists);
            Assert.True(worldBehavior2Exists);
            Assert.True(worldBehavior3Exists);

            var coords1 = database.GetWorldBehaviorByName(worldBehavior1.Name).ToDomain().FoodCoords[0];
            var coords2 = database.GetWorldBehaviorByName(worldBehavior2.Name).ToDomain().FoodCoords[0];
            var coords3 = database.GetWorldBehaviorByName(worldBehavior3.Name).ToDomain().FoodCoords[0];

            Assert.AreEqual(coords1, worldBehavior1.FoodCoords[0]);
            Assert.AreEqual(coords2, worldBehavior2.FoodCoords[0]);
            Assert.AreEqual(coords3, worldBehavior3.FoodCoords[0]);
            
            database.DeleteWorldBehavior(worldBehavior1.Name);
            database.DeleteWorldBehavior(worldBehavior2.Name);
            database.DeleteWorldBehavior(worldBehavior3.Name);
            
            worldBehavior1Exists = database.CheckWorldBehaviorExists(worldBehavior1.Name);
            worldBehavior2Exists = database.CheckWorldBehaviorExists(worldBehavior2.Name);
            worldBehavior3Exists = database.CheckWorldBehaviorExists(worldBehavior3.Name);

            Assert.False(worldBehavior1Exists);
            Assert.False(worldBehavior2Exists);
            Assert.False(worldBehavior3Exists);
        }
        
        [Test]
        public void SameWormsLogicTest()
        {
            var foodPattern = new List<(int, int)>();
            foodPattern.Add((5, 0));
            foodPattern.Add((0, 0));
            foodPattern.Add((-5, 0));
            foodPattern.Add((0, 0));
            foodPattern.Add((0, 5));
            foodPattern.Add((0, 0));
            foodPattern.Add((0, -5));
            foodPattern.Add((0, 0));

            var foodList = new List<(int, int)>();

            for (int i = 0; i != GameContract.NumberOfSteps / foodPattern.Count + 1; i++)
            {
                foodList.AddRange(foodPattern.ToList());    
            }
            
            foodList.RemoveRange(GameContract.NumberOfSteps, foodList.Count - GameContract.NumberOfSteps);
            
            Console.WriteLine(foodList.Count);
            
            ConsoleApp1.WorldBehavior testWorldBehavior = new ConsoleApp1.WorldBehavior("testSameBehavior", foodList);
            World world = new World(testWorldBehavior, new RandomNameGenerator(new Random()), new OptionalLogic(), null);
            
            
        }
    }
}