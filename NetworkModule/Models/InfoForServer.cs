using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsoleApp1.Network.Entity
{
    public partial class InfoForServer: IWorldInfoProvider
    {
        [JsonProperty("worms")]
        public List<WormDTO> Worms { get; set; }

        [JsonProperty("food")]
        public List<FoodDTO> Food { get; set; }

        public List<IWormInfoProvider> ProvideWorms()
        {
            List<IWormInfoProvider> result = new List<IWormInfoProvider>(Worms);
            return result;
        }

        public List<IFoodInfoProvider> ProvideFood()
        {
            List<IFoodInfoProvider> result = new List<IFoodInfoProvider>(Food);
            return result;
        }

        public int ProvideGameIteration()
        {
            return 0;
        }
    }
}