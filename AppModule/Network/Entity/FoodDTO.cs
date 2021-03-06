using Newtonsoft.Json;

namespace ConsoleApp1.Network.Entity
{
    public class FoodDTO
    {
        [JsonProperty("expiresIn")]
        public int ExpiresIn { get; set; }

        [JsonProperty("position")]
        public PositionDTO Position { get; set; }

        public FoodDTO(int expiresIn, (int, int) position)
        {
            ExpiresIn = expiresIn;
            Position = new PositionDTO(position.Item1, position.Item2);
        }
    }
}