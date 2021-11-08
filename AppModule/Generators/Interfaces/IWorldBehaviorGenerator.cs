using System;
using ConsoleApp1.CoreGame.Domain;

namespace ConsoleApp1.Generators
{
    /// <summary>
    ///     Интерфейс для генерации поведений мира.
    /// </summary>
    public interface IWorldBehaviorGenerator
    {
        /// <summary>
        ///     Метод для генерации поведений мира.
        /// </summary>
        /// <param name="name">
        ///     Уникальное имя поведения мира <c>WorldBehavior</c>
        /// </param>
        /// <returns>
        ///     Возвращает объект класса WorldBehavior с именем из параметра и рандомным листом координат для спавна еды.
        /// </returns>
        public WorldBehavior Generate(string name);
    }
}