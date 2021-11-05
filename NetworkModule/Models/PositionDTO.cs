using Newtonsoft.Json;

namespace ConsoleApp1.Network.Entity
{
    public partial class PositionDTO
    {
        
        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }
        
    }
}