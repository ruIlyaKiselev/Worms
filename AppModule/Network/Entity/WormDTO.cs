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

        public WormDTO(string name, int lifeStrength, (int, int) position)
        {
            Name = name;
            LifeStrength = lifeStrength;
            Position = new PositionDTO(position.Item1, position.Item2);
        }
    }
}