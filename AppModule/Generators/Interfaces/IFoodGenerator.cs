using ConsoleApp1.CoreGame.Domain;
using ConsoleApp1.CoreGame.Interfaces;

namespace ConsoleApp1.Generators
{
    /// <summary>
    ///     Интерфейс для генерации еды.
    /// </summary>
    public interface IFoodGenerator
    {
        /// <summary>
        ///     Метод для генерации еды.
        /// </summary>
        /// <param name="worldInfoProvider">
        ///     интерфейс информации о мире <c>IWorldInfoProvider</c>.
        /// </param>
        /// <returns>
        ///     Возвращает объект класса Food с уникальными координатами.
        /// </returns>
        public Food GenerateFood(IWorldInfoProvider worldInfoProvider);
    }
}