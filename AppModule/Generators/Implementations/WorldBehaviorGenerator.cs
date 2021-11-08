using ConsoleApp1.CoreGame.Domain;

namespace ConsoleApp1.Generators
{
    /// <summary>
    ///     Класс реализация интерфейса <c>IWorldBehaviorGenerator</c> для генерации поведения мира.
    /// </summary>
    public class WorldBehaviorGenerator: IWorldBehaviorGenerator
    {
        public WorldBehavior Generate(string name)
        {
            return new WorldBehavior(name);
        }
    }
}