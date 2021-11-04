using ConsoleApp1;
using ConsoleApp1.Generators;
using NUnit.Framework;

namespace TestProject1.WorldBehavior
{
    public class WorldBehaviorTest
    {
        [Test]
        public void WorldCoordsAreNotEqualsForFoodLifetimePeriod()
        {
            IWorldBehaviorGenerator worldBehaviorGenerator = new WorldBehaviorGenerator();
            var world = worldBehaviorGenerator.generate("test_world");

            var coordsList = world.FoodCoords;

            int foodLifetime = GameContract.StartWormHealth / GameContract.HealthDecreasePerIteration;

            for (int i = 0; i != world.FoodCoords.Count - foodLifetime; i++)
            {
                for (int j = 1; j != foodLifetime; j++)
                {
                    if (coordsList[i] == coordsList[i + j])
                    {
                        Assert.Fail();
                    }
                }
            }
        }
    }
}