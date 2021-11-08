using System;
using System.Linq;
using ConsoleApp1;
using ConsoleApp1.CoreGame.Domain;
using ConsoleApp1.Generators;
using ConsoleApp1.WormsLogic;
using NUnit.Framework;

namespace TestProject1.FoodGenerators
{
    public class FoodGenerationTest
    {
        
        [Test]
        public void UniqueFoodPositionsTest()
        {
            World world = new World(
                new FoodGenerator(), 
                new RandomNameGenerator(new Random()), 
                new OptionalLogic(), 
                null,
                null
            );
            IFoodGenerator generator = new FoodGenerator();
            for (var i = 0; i < 100; i++)
            {
                var newFood = generator.GenerateFood(world);
                var storedFoodCoords = world.GetFood().Select(food => food.ProvidePosition()).ToList();
                Assert.AreEqual(storedFoodCoords.Contains(newFood.CurrentPosition), false);
                world.GetFood().Add(newFood);
            }
        }
        
        [Test]
        public void AddFoodToWormPositionTest()
        {
            Worm worm = new Worm((0, 0), "test", new OptionalLogic());
            Food food = new Food((0, 0));
            
            World world = new World(
                new FoodGenerator(), 
                new RandomNameGenerator(new Random()), 
                new OptionalLogic(), 
                null,
                null
            );
            
            world.AddWorm(worm);
            
            Assert.AreEqual((world.GetWorms()[0].Health), 10);
            Assert.AreEqual((world.GetFood().Count), 0);
            Assert.AreEqual((world.GetWorms().Count), 1);
            
            world.AddFood(food);
            world.DecreaseHealths();
            
            Assert.AreEqual((world.GetWorms()[0].Health), 19);
            Assert.AreEqual((world.GetFood().Count), 0);
            Assert.AreEqual((world.GetWorms().Count), 1);
        }
    }
}