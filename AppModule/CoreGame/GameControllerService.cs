using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApp1.CoreGame.Domain;
using ConsoleApp1.Generators;
using ConsoleApp1.Logging;
using ConsoleApp1.Repository;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp1.CoreGame
{
    /// <summary>
    ///     Класс-сервис, который хранит в себе игровой мир и проводит с ним основные манипуляции.
    /// </summary>
    public class GameControllerService: IHostedService
    {
        private readonly IFoodGenerator _foodGenerator;
        private readonly INameGenerator _nameGenerator;
        private readonly IWormLogic _wormLogic;
        private readonly ILogger _logger;
        private IRepository _repository;

        private readonly World _world;
        private readonly List<(int, int)> _foodSequence;
        private string wormBehaviorName;
        
        /// <summary>
        ///     Конструктор <c>GameControllerService</c> принимает генераторы еды и имён, логику червей,
        ///     по которой мир будет принимать решение для каждого червя по умолчанию (без решений, полученных с API),
        ///     логгер и репозиторий (прослойка между БД и сетевой частью).
        /// </summary>
        /// <param name="foodGenerator">
        ///     Имплементация интерфейса <c>IFoodGenerator</c>, служит для генерации еды с уникальными координатами.
        /// </param>
        /// <param name="nameGenerator">
        ///     Имплементация интерфейса <c>INameGenerator</c>, служит для генерации уникальных имён.
        /// </param>
        /// <param name="wormLogic">
        ///     Имплементация интерефейса <c>IWormLogic</c>, служит для принятия решений о действиях червей.
        /// </param>
        /// <param name="logger">
        ///     Имплементация интерефейса <c>ILogger</c>, служит для логирования состояния мира на каждому ходе.
        /// </param>
        /// <param name="repository">
        ///     Имплементация интерефейса <c>IRepository</c>, служит для взятия данных из сети или из БД.
        /// </param>
        /// <param name="applicationLifetime">
        ///     Параметр, необходимый для работы сервиса.
        /// </param>
        /// <param name="integrationService">
        ///     Класс, необходимый для передачи параметров в данный класс-сервис.
        ///     Передает имя поведения мира, которое нужно брать из БД
        /// </param>
        public GameControllerService(
            IFoodGenerator foodGenerator,
            INameGenerator nameGenerator,
            IWormLogic wormLogic,
            ILogger logger,
            IRepository repository,
            IHostApplicationLifetime applicationLifetime,
            IntegrationService integrationService
        ) 
        {
            _foodGenerator = foodGenerator;
            _nameGenerator = nameGenerator;
            _wormLogic = wormLogic;
            _logger = logger;
            _repository = repository;
            
            _world = new World(foodGenerator, nameGenerator, wormLogic, logger, repository);
            wormBehaviorName = integrationService.WormBehaviorName;
            _foodSequence = repository.GetWorldBehaviorByName(wormBehaviorName).FoodCoords;
        }
        
        /// <summary>
        ///     Метод для старта процесса симуляции мира.
        ///     Добавляет первого червя в (0, 0) и прогоняет число итераций мира в соответствии с правилами игры.
        /// </summary>
        private void GameProcess()
        {
            _world.AddWorm(new Worm((0, 0), _nameGenerator.Generate(), _wormLogic));

            for (int i = 0; i != GameContract.NumberOfSteps; i++)
            {
                _world.AddFood(_foodSequence == null ? _foodGenerator.GenerateFood(_world) : new Food(_foodSequence[i]));
                _world.DecideWormsIntents();
                _world.DecreaseHealths();
                _world.IncrementIteration();
                _logger.LogNewEvent(_world);
            }
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
    }
}