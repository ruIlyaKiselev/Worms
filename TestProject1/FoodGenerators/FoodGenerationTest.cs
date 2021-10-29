using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1;
using ConsoleApp1.Generators;
using NUnit.Framework;

namespace TestProject1.FoodGenerators
{
    public class FoodGenerationTest
    {
        private class TmpInfoProvider: IWorldInfoProvider
        {
            public List<Food> food = new();
            public List<Worm> worms = new();
            public List<IWormInfoProvider> ProvideWorms()
            {
                return worms.Cast<IWormInfoProvider>().ToList();
            }

            public List<IFoodInfoProvider> ProvideFood()
            {
                return food.Cast<IFoodInfoProvider>().ToList();
            }

            public GameField ProvideGameField()
            {
                return null;
            }

            public int ProvideGameIteration()
            {
                return 0;
            }
        }
        
        [Test]
        public void UniqueFoodPositionsTest()
        {
            TmpInfoProvider tmpInfoProvider = new TmpInfoProvider();
            IFoodGenerator generator = new FoodGenerator();
            for (var i = 0; i < 100; i++)
            {
                var newFood = generator.GenerateFood(tmpInfoProvider);
                Assert.AreEqual(tmpInfoProvider.food.Contains(newFood), false);
                tmpInfoProvider.food.Add(newFood);
            }
        }
        
        [Test]
        public void GetNewFoodTest_FoodOnWorm_FoodEaten()
        {
            // var namesGenerator = new NamesGenerator();
            // var worm = new Worm(namesGenerator.Generate(), 0, 0);
            // var foodGenerator = new CustomFoodGenerator(new List<Coord> {new() {X = 0, Y = 0}});
            // var world = new World(foodGenerator, namesGenerator, new List<Worm> {worm});
            // new CommandFactory(world, new MockLogger())
            //     .NothingCommand().Invoke(1);
            // Assert.AreEqual(world.GetWormById(1).GetVitality(), 19);
        }
    }
}