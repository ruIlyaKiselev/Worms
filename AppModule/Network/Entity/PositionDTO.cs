using Newtonsoft.Json;

namespace ConsoleApp1.Network.Entity
{
    public partial class PositionDTO
    {
        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }

        public PositionDTO(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public PositionDTO((int, int) coords)
        {
            X = coords.Item1;
            Y = coords.Item2;
        }
    }
}