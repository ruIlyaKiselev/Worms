using System;
using System.Linq;
using ConsoleApp1;
using ConsoleApp1.Database;
using ConsoleApp1.Generators;
using ConsoleApp1.Repository;
using ConsoleApp1.WormsLogic;
using NUnit.Framework;

namespace TestProject1.WormLogic
{
    public class WormLogicTest
    {
        [Test]
        public void FindSingleFood()
        {
            World world = new World(
                new FoodGenerator(), 
                new RandomNameGenerator(new Random()), 
                new OptionalLogic(), 
                null,
                null
            );
            
            world.AddWorm(new Worm((0, 0), "test", new OptionalLogic()));
            world.AddFood(new Food((2, 0)));
            
            Assert.AreEqual(world.GetWorms().First().CurrentPosition, (0, 0));
            Assert.AreEqual(world.GetWorms().First().Health, 10);

            int stepCounter = 0;
            
            while (world.GetFood().Count > 0 && world.GetWorms().First().CurrentPosition != world.GetFood().First().CurrentPosition)
            {
                world.MoveWorm(world.GetWorms().First(), world.GetWorms().First().GetIntent(world).Item2);
                world.DecreaseHealths();
                stepCounter++;
            }
            
            Assert.AreEqual(world.GetWorms().First().CurrentPosition, (2, 0));
            Assert.AreEqual(world.GetWorms().First().Health, 18);
        }
        
        [Test]
        public void FindClosestFoodBetweenTwo()
        {
            World world = new World(
                new FoodGenerator(), 
                new RandomNameGenerator(new Random()), 
                new OptionalLogic(), 
                null,
                null
            );
            
            world.AddWorm(new Worm((0, 0), "test", new OptionalLogic()));
            world.AddFood(new Food((-1, 2)));
            world.AddFood(new Food((-4, 0)));
            
            Assert.AreEqual(world.GetWorms().First().CurrentPosition, (0, 0));
            Assert.AreEqual(world.GetWorms().First().Health, 10);

            int stepCounter = 0;
            
            while (world.GetFood().Count > 1 && world.GetWorms().First().CurrentPosition != world.GetFood().First().CurrentPosition)
            {
                world.MoveWorm(world.GetWorms().First(), world.GetWorms().First().GetIntent(world).Item2);
                world.DecreaseHealths();
                stepCounter++;
            }
            
            Assert.AreEqual(world.GetWorms().First().CurrentPosition, (-1, 2));
            Assert.AreEqual(world.GetWorms().First().Health, 17);
        }
        
        [Test]
        public void FindClosestFoodWhenAppearNewFood()
        {
            World world = new World(
                new FoodGenerator(), 
                new RandomNameGenerator(new Random()), 
                new OptionalLogic(), 
                null,
                null
            );
            
            world.AddWorm(new Worm((0, 0), "test", new OptionalLogic()));
            world.AddFood(new Food((100, 0)));

            Assert.AreEqual(world.GetWorms().First().CurrentPosition, (0, 0));
            Assert.AreEqual(world.GetWorms().First().Health, 10);

            int stepCounter = 0;
            
            while (world.GetFood().Count != 0 && world.GetWorms().First().CurrentPosition != world.GetFood().First().CurrentPosition)
            {
                world.MoveWorm(world.GetWorms().First(), world.GetWorms().First().GetIntent(world).Item2);
                world.DecreaseHealths();
                stepCounter++;

                if (stepCounter == 3)
                {
                    world.AddFood(new Food((0, 0)));
                }

                if (stepCounter == 6 && world.GetWorms().First().CurrentPosition == (0, 0))
                {
                    break;
                }
            }
            
            Assert.AreEqual(world.GetWorms().First().CurrentPosition, (-0, 0));
            Assert.AreEqual(world.GetWorms().First().Health, 14);
            
            Assert.AreEqual(world.GetFood().Count, 1);
            Assert.AreEqual(world.GetFood().First().CurrentPosition, (100, 0));
        }
    }
}