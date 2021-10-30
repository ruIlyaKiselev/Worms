using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApp1.Generators;
using ConsoleApp1.Logging;
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

        private World _world;
        
        public GameControllerService(
            IFoodGenerator foodGenerator,
            INameGenerator nameGenerator,
            IWormLogic wormLogic,
            ILogger logger,
            IHostApplicationLifetime applicationLifetime
        ) 
        {
            _foodGenerator = foodGenerator;
            _nameGenerator = nameGenerator;
            _wormLogic = wormLogic;
            _logger = logger;
            
            _world = new World(foodGenerator, nameGenerator, wormLogic, logger);
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
                DecideWormsIntents();
                _world.DecreaseHealths();
                _world.AddFood(_foodGenerator.GenerateFood(_world));
                _logger.LogNewEvent(_world);
            }
        }
        
        private void DecideWormsIntents()
        {
            foreach (var worm in new List<Worm>(_world.GetWorms()))
            {
                var wormX = worm.CurrentPosition.Item1;
                var wormY = worm.CurrentPosition.Item2;
                var (wormAction, wormDirection) = worm.GetIntent(_world);

                if (wormAction == Actions.Move)
                {
                    switch (wormDirection)
                    {
                        case Directions.Top:
                            if (_world.CheckCeil((wormX, wormY + 1)) != FieldObjects.Worm)
                            {
                                worm.CurrentPosition = (wormX, wormY + 1);
                            } break;
                        case Directions.Bottom:
                            if (_world.CheckCeil((wormX, wormY - 1)) != FieldObjects.Worm)
                            {
                                worm.CurrentPosition = (wormX, wormY - 1);
                            } break;
                        case Directions.Right:
                            if (_world.CheckCeil((wormX + 1, wormY)) != FieldObjects.Worm)
                            {
                                worm.CurrentPosition = (wormX + 1, wormY);
                            } break;
                        case Directions.Left:
                            if (_world.CheckCeil((wormX - 1, wormY)) != FieldObjects.Worm)
                            {
                                worm.CurrentPosition = (wormX - 1, wormY);
                            } break;
                    }
                }
                else if (wormAction == Actions.Budding)
                {
                    switch (wormDirection)
                    {
                        case Directions.Top:
                            if (_world.CheckCeil((wormX, wormY + 1)) == FieldObjects.Empty)
                            {
                                _world.AddWorm(new Worm((wormX, wormY + 1), _nameGenerator.Generate(), _wormLogic));
                                worm.Health -= 10;
                            } break;
                        case Directions.Bottom:
                            if (_world.CheckCeil((wormX, wormY - 1)) == FieldObjects.Empty)
                            {
                                _world.AddWorm(new Worm((wormX, wormY - 1), _nameGenerator.Generate(), _wormLogic));
                                worm.Health -= 10;
                            } break;
                        case Directions.Right:
                            if (_world.CheckCeil((wormX + 1, wormY)) == FieldObjects.Empty)
                            {
                                _world.AddWorm(new Worm((wormX + 1, wormY), _nameGenerator.Generate(), _wormLogic));
                                worm.Health -= 10;
                            } break;
                        case Directions.Left:
                            if (_world.CheckCeil((wormX - 1, wormY)) == FieldObjects.Empty)
                            {
                                _world.AddWorm(new Worm((wormX - 1, wormY), _nameGenerator.Generate(), _wormLogic));
                                worm.Health -= 10;
                            } break;
                    }
                }

                if (CheckFoodEat((wormX, wormY)))
                {
                    worm.Health += 10;
                }
            }
        }
        private bool CheckFoodEat((int, int) wormPosition)
        {
            foreach (var food in _world.GetFood())
            {
                if (food.CurrentPosition.Item1 == wormPosition.Item1 &&
                    food.CurrentPosition.Item2 == wormPosition.Item2)
                {
                    _world.GetFood().Remove(food);
                    return true;
                }
            }

            return false;
        }
    }
}