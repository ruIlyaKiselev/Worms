using ConsoleApp1.CoreGame.Interfaces;

namespace ConsoleApp1.Logging
{
    /// <summary>
    ///     Интерфейс для логирования.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        ///     Метод для логирования итерации мира.
        ///     Не требует параметров, но в этом случае мир должен быть передан в
        ///     конструктор класса-имплементатора при создании.
        /// </summary>
        public void LogNewEvent();
        /// <summary>
        ///     Метод для логирования итерации мира.
        /// </summary>
        /// <param name="infoProvider">
        ///     Интерфейс для получения необходимой информации о мире
        /// </param>
        public void LogNewEvent(IWorldInfoProvider infoProvider);
    }
}