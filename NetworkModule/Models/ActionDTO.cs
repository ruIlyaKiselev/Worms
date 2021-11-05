using Newtonsoft.Json;

namespace ConsoleApp1.Network.Entity
{
    public class ActionDTO
    {
        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("split")]
        public bool Split { get; set; }

        public ActionDTO(string direction, bool split)
        {
            Direction = direction;
            Split = split;
        }
    }
}