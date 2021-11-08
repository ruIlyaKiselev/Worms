using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.Database.Entities
{
    /// <summary>
    ///     Класс-Entity <c>WorldBehaviorEntity</c> для сохранения поведения мира в базе данных. 
    /// </summary>
    public class WorldBehaviorEntity
    {
        /// <value>Property <c>Name</c> хранит в себе имя поведения мира в таблице БД.</value>
        [Key]
        public string Name { get; set; }
        /// <value>
        ///     Property <c>FoodCoords</c> хранит в себе координаты спавна еды в мире.
        ///     Является сериализованным Json в строку. (с помощью AppModule/Database/Converters)
        /// </value>
        public string FoodCoords { get; set; }

        /// <summary>
        ///     Конструктор <c>WorldBehaviorEntity</c>. Принимает имя и сериализованный в строку лист (int, int).
        /// </summary>
        /// <param name="name">
        ///     Строка-имя мира поведения мира в таблице.
        /// </param>
        /// <param name="foodCoords">
        ///     Строка, хранящая лист (int, int), сериализованный в Json и сохраненный в виде строки
        ///     (с помощью AppModule/Database/Converters).
        /// </param>
        public WorldBehaviorEntity(string name, string foodCoords)
        {
            Name = name;
            FoodCoords = foodCoords;
        }
    }
}