using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Generators;
using ConsoleApp1.Logging;
using ConsoleApp1.WormsLogic;

namespace ConsoleApp1
{
    public class GameController: IWorldInfoProvider
    {
        private List<Worm> _worms = new();
        private List<Food> _food = new();
        private GameField _gameField = new();
        private Logger _logger;
        private int _gameIterationCounter;
        private RandomNameGenerator _randomNumberGenerator;

        public GameController()
        {
            _logger = new Logger(this);
            _randomNumberGenerator = new RandomNameGenerator(new Random());
        }
        
        public void GameProcess()
        {
            AddWorm((0, 0));

            for (_gameIterationCounter = 0; _gameIterationCounter != GameContract.NumberOfSteps; _gameIterationCounter++)
            {
                DecideWormsIntents();
                DecreaseHealths();
                AddFood();
                _logger.LogNewEvent(this);
            }
        }

        private void DecideWormsIntents()
        {
            foreach (var worm in new List<Worm>(_worms))
            {
                var wormX = worm.CurrentPosition.Item1;
                var wormY = worm.CurrentPosition.Item2;
                var (wormAction, wormDirection) = worm.GetIntent(this);

                if (wormAction == Actions.Move)
                {
                    switch (wormDirection)
                    {
                        case Directions.Top:
                            if (CheckCeil((wormX, wormY + 1)) != FieldObjects.Worm)
                            {
                                worm.CurrentPosition = (wormX, wormY + 1);
                            } break;
                        case Directions.Bottom:
                            if (CheckCeil((wormX, wormY - 1)) != FieldObjects.Worm)
                            {
                                worm.CurrentPosition = (wormX, wormY - 1);
                            } break;
                        case Directions.Right:
                            if (CheckCeil((wormX + 1, wormY)) != FieldObjects.Worm)
                            {
                                worm.CurrentPosition = (wormX + 1, wormY);
                            } break;
                        case Directions.Left:
                            if (CheckCeil((wormX - 1, wormY)) != FieldObjects.Worm)
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
                            if (CheckCeil((wormX, wormY + 1)) == FieldObjects.Empty)
                            {
                                AddWorm((wormX, wormY + 1));
                                worm.Health -= 10;
                            } break;
                        case Directions.Bottom:
                            if (CheckCeil((wormX, wormY - 1)) == FieldObjects.Empty)
                            {
                                AddWorm((wormX, wormY - 1));
                                worm.Health -= 10;
                            } break;
                        case Directions.Right:
                            if (CheckCeil((wormX + 1, wormY)) == FieldObjects.Empty)
                            {
                                AddWorm((wormX + 1, wormY));
                                worm.Health -= 10;
                            } break;
                        case Directions.Left:
                            if (CheckCeil((wormX - 1, wormY)) == FieldObjects.Empty)
                            {
                                AddWorm((wormX - 1, wormY));
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

        FieldObjects CheckCeil((int, int) ceilCoords)
        {
            var foodCoordList = _food.Select(food => food.ProvidePosition()).ToList();
            var wormsCoordList = _worms.Select(worms => worms.ProvidePosition()).ToList();

            if (wormsCoordList.Contains(ceilCoords))
            {
                return FieldObjects.Worm;
            }
            
            if (foodCoordList.Contains(ceilCoords))
            {
                return FieldObjects.Food;
            }

            return FieldObjects.Empty;
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
            _worms.Add(new Worm(startCoord, _randomNumberGenerator.Generate(), new OptionalLogic()));
        }
        
        private void AddFood()
        {
            _food.Add(new Food(FoodCoordGenerator.GenerateFoodCoord(this)));
        }

        private void DecreaseHealths()
        {
            foreach (var worm in _worms)
            {
                worm.DecreaseHealth();
            }
            
            foreach (var food in _food)
            {
                food.DecreaseHealth();
            }
            
            _worms.RemoveAll(worm => worm.IsDeath);
            _food.RemoveAll(food => food.IsDeath);
        }

        public List<IWormInfoProvider> ProvideWorms()
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