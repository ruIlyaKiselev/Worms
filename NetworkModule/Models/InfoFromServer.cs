using System;
using Newtonsoft.Json;

namespace ConsoleApp1.Network.Entity
{
    public class InfoFromServer
    {
        [JsonProperty("action")]
        public ActionDTO Action { get; set; }

        public InfoFromServer(ActionDTO action)
        {
            Action = action;
        }
    }
}