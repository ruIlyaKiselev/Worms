using System;
using Newtonsoft.Json;

namespace ConsoleApp1.Network.Entity
{
    /// <summary>
    ///     DTO класс для получения информации от сервера (API)
    /// </summary>
    public class InfoFromServer
    {
        [JsonProperty("action")]
        public ActionDTO Action { get; set; }
    }
}