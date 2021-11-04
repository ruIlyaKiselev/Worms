using System.Collections.Generic;

namespace ConsoleApp1.Repository
{
    public interface IRepository
    {
        public void SaveWorldBehavior(WorldBehavior worldBehavior);
        public WorldBehavior GetWorldBehaviorByName(string name);
        public List<WorldBehavior> GetAllWorldBehaviors();
        public void UpdateWorldBehavior(WorldBehavior worldBehavior);
        public void DeleteWorldBehavior(string name);
    }
}