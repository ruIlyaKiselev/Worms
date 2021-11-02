namespace ConsoleApp1.Generators
{
    public class WorldBehaviorGenerator: IWorldBehaviorGenerator
    {
        public WorldBehavior generate(string name)
        {
            return new WorldBehavior(name);
        }
    }
}