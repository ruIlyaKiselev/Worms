using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ConsoleApp1.Network.Entity
{
    public partial class InfoForServer
    {
        [JsonProperty("worms")]
        public List<WormDTO> Worms { get; set; }

        [JsonProperty("food")]
        public List<FoodDTO> Food { get; set; }
        
        public InfoForServer(IWorldInfoProvider infoProvider)
        {
            Worms = new List<WormDTO>();
            Food = new List<FoodDTO>();
            
            var worms = infoProvider.ProvideWorms();
            
            for (int i = 0; i != worms.Count; i++)
            {
                Worms.Add(new WormDTO(
                    worms[i].ProvideName(),
                    worms[i].ProvideHealth(),
                    worms[i].ProvidePosition())
                );
            }
            
            var food = infoProvider.ProvideFood();
            for (int i = 0; i != food.Count; i++)
            {
                Food.Add(new FoodDTO(
                    food[i].ProvideHealth(),
                    food[i].ProvidePosition()
                    )
                );
            }
        }
    }
}