using System;
using System.Net.Http;
using Refit;

namespace ConsoleApp1.Network
{
    public static class NetworkServiceFactory
    {
        public static INetworkService GetNetworkService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslErrors) => true
            };
            
            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(NetworkContract.BASE_URL)
            };

            httpClient.DefaultRequestHeaders.Add("Host", "localhost:5001");
            
            INetworkService networkService = RestService.For<INetworkService>(httpClient);

            return networkService;
        }
    }
}