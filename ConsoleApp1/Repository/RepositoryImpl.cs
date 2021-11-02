using System.Collections.Generic;
using ConsoleApp1.Database;

namespace ConsoleApp1.Repository
{
    public class RepositoryImpl: IRepository
    {
        private PostgresDatabase _database;
        
        public RepositoryImpl(PostgresDatabase database)
        {
            _database = database;
        }
        
        public void SaveWorldBehavior(WorldBehavior worldBehavior)
        {
            _database.SaveWorldBehavior(worldBehavior.Name, Converters.convertListToJson(worldBehavior.FoodCoords));
        }

        public WorldBehavior GetWorldBehaviorByName(string name)
        {
            var requestResult = _database.GetWorldBehaviorByName(name);
            return new WorldBehavior(requestResult[0], Converters.convertJsonToList<(int, int)>(requestResult[1]));
        }

        /*TODO: Implement it*/
        public List<WorldBehavior> GetAllWorldBehaviors()
        {
            return new List<WorldBehavior>();
        }

        public void UpdateWorldBehavior(WorldBehavior worldBehavior)
        {
            _database.UpdateWorldBehaviorByName(worldBehavior.Name, Converters.convertListToJson(worldBehavior.FoodCoords));
        }

        public void DeleteWorldBehavior(string name)
        {
            _database.DeleteWorldBehaviorByName(name);
        }
    }
}