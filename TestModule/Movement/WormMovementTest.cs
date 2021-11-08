using System;
using System.Linq;
using ConsoleApp1;
using ConsoleApp1.CoreGame.Domain;
using ConsoleApp1.CoreGame.Enums;
using ConsoleApp1.CoreGame.Exceptions;
using ConsoleApp1.Generators;
using ConsoleApp1.WormsLogic;
using NUnit.Framework;

namespace TestProject1.Movement
{
    public class WormMovementTest
    {
        [Test]
        public void MovementToEmptyPosition()
        {
            World world = new World(
                new FoodGenerator(), 
                new RandomNameGenerator(new Random()), 
                new OptionalLogic(), 
                null,
                null
            );
            
            world.AddWorm(new Worm((0, 0), "test", new OptionalLogic()));
            
            Assert.AreEqual(world.GetWorms().First().CurrentPosition, (0, 0));
            Assert.AreEqual(world.GetWorms().First().Health, 10);
            
            world.MoveWorm(world.GetWorms().First(), Directions.Bottom);
            world.DecreaseHealths();
            
            Assert.AreEqual(world.GetWorms().First().CurrentPosition, (0, -1));
            Assert.AreEqual(world.GetWorms().First().Health, 9);
        }
        
        [Test]
        public void MovementToFoodPosition()
        {
            World world = new World(
                new FoodGenerator(), 
                new RandomNameGenerator(new Random()), 
                new OptionalLogic(), 
                null,
                null
            );
            
            world.AddWorm(new Worm((0, 0), "test", new OptionalLogic()));
            world.AddFood(new Food((0, -1)));
            
            Assert.AreEqual(world.GetWorms().First().CurrentPosition, (0, 0));
            Assert.AreEqual(world.GetWorms().First().Health, 10);
            
            world.MoveWorm(world.GetWorms().First(), Directions.Bottom);
            world.DecreaseHealths();
            
            Assert.AreEqual(world.GetWorms().First().CurrentPosition, (0, -1));
            Assert.AreEqual(world.GetWorms().First().Health, 19);
        }
        
        [Test]
        public void MovementToOtherWormPosition()
        {
            World world = new World(
                new FoodGenerator(), 
                new RandomNameGenerator(new Random()), 
                new OptionalLogic(), 
                null,
                null
            );
            world.AddWorm(new Worm((0, 0), "test", new OptionalLogic()));
            world.AddWorm(new Worm((0, -1), "test2", new OptionalLogic()));
            
            Assert.AreEqual(world.GetWorms().First().CurrentPosition, (0, 0));
            Assert.AreEqual(world.GetWorms().First().Health, 10);
            
            Assert.AreEqual(world.GetWorms().Last().CurrentPosition, (0, -1));
            Assert.AreEqual(world.GetWorms().Last().Health, 10);

            Assert.Throws<WormMovementException>(() =>
            {
                world.MoveWorm(world.GetWorms().First(), Directions.Bottom);
            });
        }
    }
}