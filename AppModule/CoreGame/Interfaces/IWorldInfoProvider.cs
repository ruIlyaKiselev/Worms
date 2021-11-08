using System.Collections.Generic;

namespace ConsoleApp1.CoreGame.Interfaces
{
    /// <summary>
    ///     Интерфейс для обеспечения всей необходимой информации о мире без нарушения инкапсуляции.
    /// </summary>
    public interface IWorldInfoProvider
    {
        /// <summary>Возвращает лист с информацией <c>IWormInfoProvider</c> о червях в мире</summary>
        List<IWormInfoProvider> ProvideWorms();
        /// <summary>Возвращает лист с информацией <c>IFoodInfoProvider</c> о еде в мире</summary>
        List<IFoodInfoProvider> ProvideFood();
        /// <summary>Возвращает текущий ход в мире</summary>
        int ProvideGameIteration();
    }
}