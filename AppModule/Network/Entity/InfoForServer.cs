using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.CoreGame.Interfaces;
using Newtonsoft.Json;

namespace ConsoleApp1.Network.Entity
{
    /// <summary>
    ///     DTO класс для отправки информации на сервер (API)
    /// </summary>
    public partial class InfoForServer
    {
        [JsonProperty("iteration")]
        public int Iteration { get; set; }
        
        [JsonProperty("worms")]
        public List<WormDTO> Worms { get; set; }

        [JsonProperty("food")]
        public List<FoodDTO> Food { get; set; }
        
        /// <summary>
        ///     Конструктор для создания InfoForServer
        /// </summary>
        /// <param name="infoProvider">
        ///     Принимает интерфейс с данными о мире IWorldInfoProvider
        /// </param>
        public InfoForServer(IWorldInfoProvider infoProvider)
        {
            Worms = new List<WormDTO>();
            Food = new List<FoodDTO>();

            Iteration = infoProvider.ProvideGameIteration();
            
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