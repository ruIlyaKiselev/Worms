using ConsoleApp1.CoreGame.Interfaces;
using Newtonsoft.Json;

namespace ConsoleApp1.Network.Entity
{
    public partial class WormDTO: IWormInfoProvider
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lifeStrength")]
        public int LifeStrength { get; set; }

        [JsonProperty("position")]
        public PositionDTO Position { get; set; }

        public string ProvideName()
        {
            return Name;
        }

        public (int, int) ProvidePosition()
        {
            return (Position.X, Position.Y);
        }

        public int ProvideHealth()
        {
            return LifeStrength;
        }
    }
}