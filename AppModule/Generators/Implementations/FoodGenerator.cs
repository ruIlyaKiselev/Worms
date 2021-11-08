using ConsoleApp1.CoreGame.Domain;
using ConsoleApp1.CoreGame.Interfaces;

namespace ConsoleApp1.Generators
{
    /// <summary>
    ///     Класс реализация интерфейса <c>IFoodGenerator</c> для генерации еды.
    /// </summary>
    public class FoodGenerator: IFoodGenerator
    {
        public Food GenerateFood(IWorldInfoProvider worldInfoProvider)
        {
            return new Food(FoodCoordGenerator.GenerateFoodCoord(worldInfoProvider));
        }
    }
}