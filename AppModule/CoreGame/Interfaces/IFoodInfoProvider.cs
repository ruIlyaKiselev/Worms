namespace ConsoleApp1.CoreGame.Interfaces
{
    /// <summary>
    ///     Интерфейс для обеспечения всей необходимой информации о еде без нарушения инкапсуляции.
    /// </summary>
    public interface IFoodInfoProvider
    {
        /// <summary>Возвращает координату еды в мире</summary>
        public (int, int) ProvidePosition();
        /// <summary>Возвращает здоровье еды в мире</summary>
        public int ProvideHealth();
    }
}