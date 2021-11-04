using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Database;

namespace ConsoleApp1.Repository
{
    public class RepositoryImpl: IRepository
    {
        private PostgresDatabaseORM _postgresDatabase;

        public RepositoryImpl(PostgresDatabaseORM database)
        {
            _postgresDatabase = database;
        }
        
        public void SaveWorldBehavior(WorldBehavior worldBehavior)
        {
            _postgresDatabase.SaveWorldBehavior(worldBehavior.ToEntity());
        }

        public WorldBehavior GetWorldBehaviorByName(string name)
        {
            return _postgresDatabase.GetWorldBehaviorByName(name).ToDomain();
        }
        
        public List<WorldBehavior> GetAllWorldBehaviors()
        {
            return _postgresDatabase.GetAllWorldBehaviors().Select(it => it.ToDomain()).ToList();
        }

        public void UpdateWorldBehavior(WorldBehavior worldBehavior)
        {
            _postgresDatabase.EditWorldBehavior(worldBehavior.Name, Converters.convertListToJson(worldBehavior.FoodCoords));
        }

        public void DeleteWorldBehavior(string name)
        {
            _postgresDatabase.DeleteWorldBehavior(name);
        }
    }
}