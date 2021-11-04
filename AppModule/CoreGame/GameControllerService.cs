using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApp1.Generators;
using ConsoleApp1.Logging;
using ConsoleApp1.Repository;
using ConsoleApp1.WormsLogic;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp1
{
    public class GameControllerService: IHostedService
    {
        private IFoodGenerator _foodGenerator;
        private INameGenerator _nameGenerator;
        private IWormLogic _wormLogic;
        private ILogger _logger;
        private IRepository _repository;

        private World _world;
        private List<(int, int)> _foodSequence;
        
        public GameControllerService(
            IFoodGenerator foodGenerator,
            INameGenerator nameGenerator,
            IWormLogic wormLogic,
            ILogger logger,
            IRepository repository,
            IHostApplicationLifetime applicationLifetime
        ) 
        {
            _foodGenerator = foodGenerator;
            _nameGenerator = nameGenerator;
            _wormLogic = wormLogic;
            _logger = logger;
            _repository = repository;
            
            _world = new World(foodGenerator, nameGenerator, wormLogic, logger);
            _foodSequence = repository.GetWorldBehaviorByName("world1").FoodCoords;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            GameProcess();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Finish");
            return Task.CompletedTask;
        }
        
        public void GameProcess()
        {
            _world.AddWorm(new Worm((0, 0), _nameGenerator.Generate(), _wormLogic));

            for (int i = 0; i != GameContract.NumberOfSteps; i++)
            {
                _world.DecideWormsIntents();
                _world.DecreaseHealths();
                _world.AddFood(_foodSequence == null ? _foodGenerator.GenerateFood(_world) : new Food(_foodSequence[i]));
                _logger.LogNewEvent(_world);
            }
        }
    }
}