namespace ConsoleApp1.Network
{
    /// <summary>
    ///     Класс-контракт для API.
    /// </summary>
    public class NetworkContract
    {
        /// <summary>
        ///     BASE_URL - базовый URL, относительно которого будут делаться API запросы.
        ///     Нужен для Refit сервиса
        /// </summary>
        public static readonly string BASE_URL = "https://localhost:8080/api";
        /// <summary>HOST - ip адрес или доменное имя хоста, для которого создается HTTP-клиент </summary>
        public static readonly string HOST = "localhost";
        /// <summary>HOST - порт хоста, для которого создается HTTP-клиент </summary>
        public static readonly int PORT = 8080;
    }
}