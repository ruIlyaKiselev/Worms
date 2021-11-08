using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Database
{
    /// <summary>
    ///     Класс ORM для подключения к БД PostgreSQL.
    ///     Поддерживает набор CRUD операций к модели БД worldBehaviorEntity.
    ///     Конфигурация находится в AppModule/Database/PostgresContract.
    /// </summary>
    public class PostgresDatabaseORM: DbContext
    {
        private DbSet<WorldBehaviorEntity> WorldBehaviors { get; set; }

        public PostgresDatabaseORM()
        {
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($@"Host={PostgresContract.Host};
                                                    Port={PostgresContract.Port};
                                                    Database={PostgresContract.DBname};
                                                    Username={PostgresContract.User};
                                                    Password={PostgresContract.Password}");
        }

        /// <summary>
        ///     Метод для получения <c>WorldBehaviorEntity</c> по имени
        /// </summary>
        /// <param name="name">
        ///     строка-имя WorldBehaviorEntity в таблице
        /// </param>
        /// <returns>
        ///     Возвращает WorldBehaviorEntity по запрашиваемому имени
        /// </returns>
        public WorldBehaviorEntity GetWorldBehaviorByName(string name)
        {
            var queryResult = WorldBehaviors
                .Where(wb => wb.Name == name)
                .ToList();

            return queryResult.Count != 0 ? queryResult.ToList().First() : null;
        }

        /// <summary>
        ///     Метод для получения всех сохраненных <c>WorldBehaviorEntity</c> 
        /// </summary>
        /// <returns>
        ///     Возвращает лист WorldBehaviorEntity
        /// </returns>
        public List<WorldBehaviorEntity> GetAllWorldBehaviors()
        {
            return WorldBehaviors.ToList();
        }

        /// <summary>
        ///     Сохраняет поведение мира worldBehaviorEntity
        /// </summary>
        /// <param name="worldBehaviorEntity">
        ///     объект класса WorldBehaviorEntity для сохранения
        /// </param>
        public void SaveWorldBehavior(WorldBehaviorEntity worldBehaviorEntity)
        {
            WorldBehaviors.Add(worldBehaviorEntity);
            SaveChanges();
        }

        /// <summary>
        ///     Редактирует поведение мира в таблице по имени
        /// </summary>
        /// <param name="name">
        ///     имя записи в БД, для которой вносятся обновленные данные
        /// </param>
        /// <param name="newValue">
        ///     новый сериализованный лист координат (Json в формате string)
        /// </param>
        public void EditWorldBehavior(string name, string newValue)
        {
            WorldBehaviors.Add(new WorldBehaviorEntity(name, newValue));
            SaveChanges();
        }

        /// <summary>
        ///     Удаляет сохранненное поведение мира WorldBehaviorEntity из БД
        /// </summary>
        /// <param name="name">
        ///     имя записи в БД, которая подлежит удалению
        /// </param>
        public void DeleteWorldBehavior(string name)
        {
            var itemToDelete = WorldBehaviors.ToList().Find(it => it.Name == name);
            if (itemToDelete != null) WorldBehaviors.Remove(itemToDelete);
            SaveChanges();
        }

        /// <summary>
        ///     Проверяет, есть ли сохранненное поведение мира WorldBehaviorEntity в БД с данным именем
        /// </summary>
        /// <param name="name">
        ///     имя записи в БД, которую ищем
        /// </param>
        /// <returns>
        ///     true, если запись найдена, иначе - false
        /// </returns>
        public bool CheckWorldBehaviorExists(string name)
        {
            return WorldBehaviors.ToList().Find(it => it.Name == name) != null;
        }
    }
}