using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApp1.CoreGame.Domain;
using ConsoleApp1.CoreGame.Enums;
using ConsoleApp1.CoreGame.Interfaces;
using ConsoleApp1.Network.Entity;

namespace ConsoleApp1.Repository
{
    /// <summary>
    ///     Интерфейс репозитория для общения с БД и с API.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        ///     Метод для сохранения поведения мира в БД
        /// </summary>
        /// <param name="worldBehavior">
        ///     Объект WorldBehavior, который будет сохранен в БД
        /// </param>
        public void SaveWorldBehavior(WorldBehavior worldBehavior);
        /// <summary>
        ///     Метод для получения поведения мира из БД по имени.
        /// </summary>
        /// <param name="name">
        ///     Имя поведения мира в таблице.
        /// </param>
        /// <returns>
        ///     Возвращает WorldBehavior с именем как в параметре.
        /// </returns>
        public WorldBehavior GetWorldBehaviorByName(string name);
        /// <summary>
        ///     Метод для получения всех поведений мира из БД.
        /// </summary>
        /// <returns>
        ///     Возвращает список всех WorldBehavior из таблицы.
        /// </returns>
        public List<WorldBehavior> GetAllWorldBehaviors();
        /// <summary>
        ///     Метод для обновления поведения мира в БД.
        /// </summary>
        /// <param name="worldBehavior">
        ///     Поведение мира, которое следует обновить.
        /// </param>
        public void UpdateWorldBehavior(WorldBehavior worldBehavior);
        /// <summary>
        ///     Метод для удаления поведения мира из БД.
        /// </summary>
        /// <param name="name">
        ///     Имя поведения мира, которое следует удалить из таблицы.
        /// </param>
        public void DeleteWorldBehavior(string name);

        /// <summary>
        ///     Метод для получения поведения червя от API.
        /// </summary>
        /// <param name="wormName">
        ///     Имя червя, для которого получаем поведение.
        /// </param>
        /// <param name="infoProvider">
        ///     Интерфейс информации о мире IWorldInfoProvider
        /// </param>
        /// <returns>
        ///     Возвращает пару (<c>Actions</c>, <c>Directions</c>) - действие и направление действия для данного червя.
        /// </returns>
        public (Actions, Directions) GetWormActionFromAPI(string wormName, IWorldInfoProvider infoProvider);
    }
}