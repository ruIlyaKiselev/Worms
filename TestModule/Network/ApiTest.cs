using System;
using System.Net.Http;
using ConsoleApp1;
using ConsoleApp1.CoreGame.Domain;
using ConsoleApp1.Generators;
using ConsoleApp1.Network;
using ConsoleApp1.Network.Entity;
using ConsoleApp1.WormsLogic;
using NUnit.Framework;
using Refit;

namespace TestProject1.Network
{
    public class ApiTest
    {
        [Test]
        public void ApiPostTest()
        {
            World world = new World(new FoodGenerator(), new RandomNameGenerator(new Random()), new OptionalLogic(), null, null);
            
            world.AddWorm(new Worm((1, 1), "John", new OptionalLogic()));
            
            world.AddFood(new Food((2, 1)));
            world.AddFood(new Food((0, 1)));
            world.AddFood(new Food((1, 0)));

            // world.GetWorms()[0].Health = 50;
            
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

            InfoForServer infoForServer = new InfoForServer(world);
            var response = networkService.GetWormAction("John", infoForServer);

            var result = response.Result.Action;
            Console.WriteLine(result.Direction);
            Console.WriteLine(result.Split);
        }
    }
}