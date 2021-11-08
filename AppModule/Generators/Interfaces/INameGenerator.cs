namespace ConsoleApp1.Generators
{
    /// <summary>
    ///     Интерфейс для генерации уникальных имён.
    /// </summary>
    public interface INameGenerator
    {
        /// <summary>
        ///     Метод для создания уникальных имён.
        /// </summary>
        /// <returns>
        ///     Возвращает строку, которая является уникальным именем червя.
        /// </returns>
        public string Generate();
    }
}