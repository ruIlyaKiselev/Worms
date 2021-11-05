using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApp1.Network.Entity;

namespace ConsoleApp1.Repository
{
    public interface IRepository
    {
        public void SaveWorldBehavior(WorldBehavior worldBehavior);
        public WorldBehavior GetWorldBehaviorByName(string name);
        public List<WorldBehavior> GetAllWorldBehaviors();
        public void UpdateWorldBehavior(WorldBehavior worldBehavior);
        public void DeleteWorldBehavior(string name);

        public (Actions, Directions) GetWormActionFromAPI(string wormName, IWorldInfoProvider infoProvider);
    }
}