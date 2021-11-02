using System;

namespace ConsoleApp1.Generators
{
    public interface IWorldBehaviorGenerator
    {
        public WorldBehavior generate(string name);
    }
}