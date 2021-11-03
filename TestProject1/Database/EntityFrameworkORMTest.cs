﻿using System;
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
            PostgresDatabaseORM database = new PostgresDatabaseORM();
            ConsoleApp1.WorldBehavior testWorldBehavior = new ConsoleApp1.WorldBehavior("same");
            
            database.SaveWorldBehavior(testWorldBehavior.ToDTO());

            ConsoleApp1.WorldBehavior restoredWorldBehavior = database.GetWorldBehaviorByName("same").ToDomain();
            
            List<(int, int)> coordHistory1 = EmulateGameProcess(InitWorld(restoredWorldBehavior), restoredWorldBehavior);
            List<(int, int)> coordHistory2 = EmulateGameProcess(InitWorld(restoredWorldBehavior), restoredWorldBehavior);
            List<(int, int)> coordHistory3 = EmulateGameProcess(InitWorld(restoredWorldBehavior), restoredWorldBehavior);
            
            Assert.AreEqual(coordHistory1.Count, coordHistory2.Count);
            Assert.AreEqual(coordHistory2.Count, coordHistory3.Count);

            for (int i = 0; i != coordHistory1.Count; i++)
            {
                Assert.AreEqual(coordHistory1[i], coordHistory2[i]);
                Assert.AreEqual(coordHistory2[i], coordHistory3[i]);
            }
            
            database.DeleteWorldBehavior(testWorldBehavior.Name);
        }

        private List<(int, int)> EmulateGameProcess(World world, ConsoleApp1.WorldBehavior worldBehavior)
        {
            List<(int, int)> coordHistory = new List<(int, int)>();
            
            for (int i = 0; i != GameContract.NumberOfSteps; i++)
            {
                world.DecideWormsIntents();
                world.DecreaseHealths();
                world.AddFood(worldBehavior.GenerateFood(world));

                if (world.ProvideWorms().Count != 0)
                {
                    for (int j = 0; j != world.ProvideWorms().Count; j++)
                    {
                        coordHistory.Add(world.ProvideWorms()[j].ProvidePosition());
                    }
                }
            }

            return coordHistory;
        }

        private World InitWorld(ConsoleApp1.WorldBehavior worldBehavior)
        {
            World world = new World(worldBehavior, new RandomNameGenerator(new Random()), new OptionalLogic(), null);
            world.AddWorm(new Worm((0, 0), "test", new OptionalLogic()));

            return world;
        }
    }
}