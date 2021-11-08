using System;
using System.Net.Http;
using Refit;

namespace ConsoleApp1.Network
{
    /// <summary>
    ///     Файбичный класс для создания имплементации API сервиса.
    /// </summary>
    public static class NetworkServiceFactory
    {
        /// <summary>
        ///     Файбичный метод, создает API сервис.
        ///     Добавляет заголовок Host к сообщению серверу
        ///     и разрешает соединения без использования TLS.
        /// </summary>
        /// <param name="baseUrl">
        ///     базовый URL, относительно которого будут делаться API запросы
        /// </param>
        /// <param name="host">
        ///     ip адрес или доменное имя хоста, для которого создается HTTP-клиент
        /// </param>
        /// <param name="host">
        ///     порт хоста, для которого создается HTTP-клиент
        /// </param>
        /// <returns>
        ///     Возвращает имплементацию INetworkService с учетом входных параметров.
        /// </returns>
        public static INetworkService GetNetworkService(string baseUrl, string host, int port)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslErrors) => true
            };
            
            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseUrl)
            };

            httpClient.DefaultRequestHeaders.Add("Host", $"{host}:{port}");
            
            INetworkService networkService = RestService.For<INetworkService>(httpClient);

            return networkService;
        }
    }
}