using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsoleApp1.Network.Entity
{
    public partial class InfoForServer
    {
        [JsonProperty("worms")]
        public List<Worm> Worms { get; set; }

        [JsonProperty("food")]
        public List<Food> Food { get; set; }
    }
}