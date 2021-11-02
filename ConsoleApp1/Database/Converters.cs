using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsoleApp1.Database
{
    public static class Converters
    {
        public static string convertListToJson<T>(List<T> listToConvert)
        {
            return JsonConvert.SerializeObject(listToConvert, Formatting.Indented);
        }
        
        public static List<T> convertJsonToList<T>(string jsonToConvert)
        {
            return JsonConvert.DeserializeObject<List<T>>(jsonToConvert);
        }
    }
}