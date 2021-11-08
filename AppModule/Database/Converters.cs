using System.Collections.Generic;
using ConsoleApp1.CoreGame.Domain;
using ConsoleApp1.Database.Entities;
using Newtonsoft.Json;

namespace ConsoleApp1.Database
{
    /// <summary>
    ///     Утильный класс-конвертер <c>Converters</c>
    ///     хранит методы для сериализации и десериализации листа координат в string
    ///     и для перевода доменной модели поведения мира в модель БД. 
    /// </summary>
    public static class Converters
    {
        /// <summary>
        ///     Метод для конвертирования листа произвольного типа в Json строку
        /// </summary>
        /// <param name="listToConvert">
        ///     Лист произвольного типа T
        /// </param>
        /// <returns>
        ///     Возвращает строку, в которой находится сериализованный Json в формате string.
        /// </returns>
        public static string convertListToJson<T>(List<T> listToConvert)
        {
            return JsonConvert.SerializeObject(listToConvert, Formatting.Indented);
        }
        
        /// <summary>
        ///     Метод для конвертирования Json в формате string в лист произвольного типа
        /// </summary>
        /// <param name="jsonToConvert">
        ///     Json в формате string, в котором хранится список произвольного типа.
        /// </param>
        /// <returns>
        ///     Возвращает лист произвольного типа, который десериализован из входного Json файла.
        /// </returns>
        public static List<T> convertJsonToList<T>(string jsonToConvert)
        {
            return JsonConvert.DeserializeObject<List<T>>(jsonToConvert);
        }

        /// <summary>
        ///     Extension-mapper из доменной модели WorldBehavior в модель БД WorldBehaviorEntity.
        /// </summary>
        /// <param name="worldBehavior">
        ///     Доменная модель WorldBehavior.
        /// </param>
        /// <returns>
        ///     Возвращает модель БД WorldBehaviorEntity с данными из входной worldBehavior.
        /// </returns>
        public static WorldBehaviorEntity ToEntity(this WorldBehavior worldBehavior)
        {
            return new WorldBehaviorEntity(worldBehavior.Name, convertListToJson(worldBehavior.FoodCoords));
        }
        
        /// <summary>
        ///     Extension-mapper из модели БД WorldBehaviorEntity в доменную модель WorldBehavior.
        /// </summary>
        /// <param name="worldBehaviorEntity">
        ///     Модель БД WorldBehaviorEntity.
        /// </param>
        /// <returns>
        ///     Возвращает доменную модель WorldBehavior с данными из входной worldBehaviorEntity.
        /// </returns>
        public static WorldBehavior ToDomain(this WorldBehaviorEntity worldBehaviorEntity)
        {
            return new WorldBehavior(worldBehaviorEntity.Name, convertJsonToList<(int, int)>(worldBehaviorEntity.FoodCoords));
        }
    }
}