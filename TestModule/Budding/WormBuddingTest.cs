using System;
using System.Linq;
using ConsoleApp1;
using ConsoleApp1.Exceptions;
using ConsoleApp1.Generators;
using ConsoleApp1.WormsLogic;
using NUnit.Framework;

namespace TestProject1.Budding
{
    public class WormBuddingTest
    {
        [Test]
        public void BuddingToEmptyPosition()
        {
            World world = new World(
                new FoodGenerator(), 
                new RandomNameGenerator(new Random()), 
                new OptionalLogic(), 
                null
            );
            
            world.AddWorm(new Worm((0, 0), "test", new OptionalLogic()));
            world.GetWorms().First().Health += 10;
            
            Assert.AreEqual(world.GetWorms().First().CurrentPosition, (0, 0));
            Assert.AreEqual(world.GetWorms().First().Health, 20);
            
            world.BudWorm(world.GetWorms().First(), Directions.Bottom);
            world.DecreaseHealths();
            
            Assert.AreEqual(world.GetWorms().First().CurrentPosition, (0, 0));
            Assert.AreEqual(world.GetWorms().First().Health, 9);
            
            Assert.AreEqual(world.GetWorms().Last().CurrentPosition, (0, -1));
            Assert.AreEqual(world.GetWorms().Last().Health, 9);
        }
        
        [Test]
        public void BuddingToFoodPosition()
        {
            World world = new World(
                new FoodGenerator(), 
                new RandomNameGenerator(new Random()), 
                new OptionalLogic(), 
                null
            );
            
            world.AddWorm(new Worm((0, 0), "test", new OptionalLogic()));
            world.AddFood(new Food((0, -1)));

            world.GetWorms().First().Health += 10;
            
            Assert.AreEqual(world.GetWorms().First().CurrentPosition, (0, 0));
            Assert.AreEqual(world.GetWorms().First().Health, 20);

            Assert.Throws<WormBuddingException>(() =>
            {
                world.BudWorm(world.GetWorms().First(), Directions.Bottom);
            });
        }
        
        [Test]
        public void BuddingToOtherWormPosition()
        {
            World world = new World(
                new FoodGenerator(), 
                new RandomNameGenerator(new Random()), 
                new OptionalLogic(), 
                null
            );
            
            world.AddWorm(new Worm((0, 0), "test", new OptionalLogic()));
            world.AddWorm(new Worm((0, -1), "test2", new OptionalLogic()));

            world.GetWorms().First().Health += 10;
            
            Assert.AreEqual(world.GetWorms().First().CurrentPosition, (0, 0));
            Assert.AreEqual(world.GetWorms().First().Health, 20);
            
            Assert.AreEqual(world.GetWorms().Last().CurrentPosition, (0, -1));
            Assert.AreEqual(world.GetWorms().Last().Health, 10);

            Assert.Throws<WormBuddingException>(() =>
            {
                world.BudWorm(world.GetWorms().First(), Directions.Bottom);
            });
        }
    }
}