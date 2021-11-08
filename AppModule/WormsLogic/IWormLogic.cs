using ConsoleApp1.CoreGame.Enums;
using ConsoleApp1.CoreGame.Interfaces;

/// <summary>
///     Интерфейс для принятия решений червя (логика действий червя).
/// </summary>
public interface IWormLogic
{
    /// <summary>
    ///     Метод для принятия решений червя.
    /// </summary>
    /// <param name="worm">
    ///     Интерфейс информации о черве, для которого принимается решение.
    /// </param>
    /// <param name="infoProvider">
    ///     Интерфейс информации о мире <c>IWorldInfoProvider</c>.
    /// </param>
    /// <returns>
    ///     Возвращает пару (<c>Actions</c>, <c>Directions</c>),
    ///     где <c>Actions</c> - действие червя, <c>Directions</c> - направление действия.
    /// </returns>
    public (Actions, Directions) Decide(IWormInfoProvider worm, IWorldInfoProvider infoProvider);
}