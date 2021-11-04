using Newtonsoft.Json;

namespace ConsoleApp1.Network.Entity
{
    public partial class WormDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lifeStrength")]
        public int LifeStrength { get; set; }

        [JsonProperty("position")]
        public PositionDTO Position { get; set; }
    }
}