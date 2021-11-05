using Newtonsoft.Json;

namespace ConsoleApp1.Network.Entity
{
    public class FoodDTO: IFoodInfoProvider
    {
        [JsonProperty("expiresIn")]
        public int ExpiresIn { get; set; }

        [JsonProperty("position")]
        public PositionDTO Position { get; set; }

        public (int, int) ProvidePosition()
        {
            return (Position.X, Position.Y);
        }

        public int ProvideHealth()
        {
            return ExpiresIn;
        }
    }
}