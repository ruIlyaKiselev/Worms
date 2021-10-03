using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Generators;
using ConsoleApp1.Logging;

namespace ConsoleApp1
{
    public class GameController: IWorldInfoProvider
    {
        private List<Worm> _worms = new();
        private List<Food> _food = new();
        private GameField _gameField = new();
        private Logger _logger;
        private int _gameIterationCounter;

        public GameController()
        {
            _logger = new Logger(this);
        }
        
        public void GameProcess()
        {
            AddWorm((0, 0));
            
            for (_gameIterationCounter = 0; _gameIterationCounter != GameContract.NumberOfSteps; _gameIterationCounter++)
            {
                AddFood();
                _gameField.UpdateField(_worms.AsReadOnly(), _food.AsReadOnly());
                DecideWormsIntents();
                _gameField.UpdateField(_worms.AsReadOnly(), _food.AsReadOnly());
                _gameField.PrintField();
                DecreaseHealths();
                _logger.LogNewEvent();
            }
        }
        
        private void AddFood()
        {
            _food.Add(new Food(FoodCoordGenerator.GenerateFoodCoord(_gameField)));
        }

        private void DecreaseHealths()
        {
            foreach (var food in _food)
            {
                food.DecreaseHealth();
                if (food.IsDeath)
                {
                    _food.Remove(food);
                }
            }

            foreach (var worm in _worms)
            {
                worm.DecreaseHealth();
                if (worm.IsDeath)
                {
                    _worms.Remove(worm);
                }
            }
        }

        private void DecideWormsIntents()
        {
            var gameField = _gameField.GetFieldAsArray();
            foreach (var worm in _worms)
            {
                Console.WriteLine(worm.CurrentPosition.ToString());
                Console.WriteLine("Health: " + worm.Health);
                var wormX = worm.CurrentPosition.Item1;
                var wormY = worm.CurrentPosition.Item2;
                var wormIntent = worm.GetIntent(_gameField);

                if (wormIntent.Item1 == Actions.Move)
                {
                    switch (wormIntent.Item2)
                    {
                        case Directions.Top:
                            if (_gameField.CheckCeil((wormX, wormY + 1)) != FieldObjects.Worm)
                            {
                                worm.CurrentPosition = (wormX, wormY + 1);
                            } break;
                        case Directions.Bottom:
                            if (_gameField.CheckCeil((wormX, wormY - 1)) != FieldObjects.Worm)
                            {
                                worm.CurrentPosition = (wormX, wormY - 1);
                            } break;
                        case Directions.Right:
                            if (_gameField.CheckCeil((wormX + 1, wormY)) != FieldObjects.Worm)
                            {
                                worm.CurrentPosition = (wormX + 1, wormY);
                            } break;
                        case Directions.Left:
                            if (_gameField.CheckCeil((wormX - 1, wormY)) != FieldObjects.Worm)
                            {
                                worm.CurrentPosition = (wormX - 1, wormY);
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
            foreach (var food in _food)
            {
                if (food.CurrentPosition.Item1 == wormPosition.Item1 &&
                    food.CurrentPosition.Item2 == wormPosition.Item2)
                {
                    _food.Remove(food);
                    return true;
                }
            }

            return false;
        }
        
        private void AddWorm((int, int) startCoord)
        {
            _worms.Add(new Worm(startCoord, "name"));
        }

        public List<IWormInfoProvider> ProvideWormsInfo()
        {
            return _worms.Cast<IWormInfoProvider>().ToList();
        }

        public List<IFoodInfoProvider> ProvideFood()
        {
            return _food.Cast<IFoodInfoProvider>().ToList();
        }

        public GameField ProvideGameField()
        {
            return (GameField)_gameField.Clone();
        }

        public int ProvideGameIteration()
        {
            return _gameIterationCounter;
        }
    }
}