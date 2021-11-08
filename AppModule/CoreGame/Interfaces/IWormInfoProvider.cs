namespace ConsoleApp1.CoreGame.Interfaces
{
    /// <summary>
    ///     Интерфейс для обеспечения всей необходимой информации о черве без нарушения инкапсуляции.
    /// </summary>
    public interface IWormInfoProvider
    {
        /// <summary>Возвращает имя червя</summary>
        public string ProvideName();
        /// <summary>Возвращает координату червя в мире</summary>
        public (int, int) ProvidePosition();
        /// <summary>Возвращает здоровье червя в мире</summary>
        public int ProvideHealth();
    }
}