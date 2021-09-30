using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using ConsoleApp1.Generators;

namespace ConsoleApp1
{
    public class GameController
    {
        private List<Worm> _worms = new List<Worm>();
        private List<Food> _food = new List<Food>();
        private GameField _gameField = new GameField();
        RandomName randomName = new RandomName(new Random(DateTime.Now.Second));
        
        
        public void GameProcess()
        {
            AddWorm();
            for (int i = 0; i != GameContract.NumberOfSteps; i++)
            {
                AddFood();
                _gameField.UpdateField(_worms.AsReadOnly(), _food.AsReadOnly());
                DecideWormsIntents();
                _gameField.UpdateField(_worms.AsReadOnly(), _food.AsReadOnly());
                _gameField.PrintField();
            }

            DecreaseHealths();
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
                if (worm.IsDeath())
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
                int wormX = worm.CurrentPosition.Item1;
                int wormY = worm.CurrentPosition.Item2;
                var wormIntent = worm.GetIntent(_gameField);

                if (wormIntent.Item1 == Actions.Move)
                {
                    switch (wormIntent.Item2)
                    {
                        case Directions.Top:
                            if (_gameField.CheckCeil((wormX, wormY + 1)) != FieldObjects.Worm)
                            {
                                worm.CurrentPosition = (wormX, worm.CurrentPosition.Item2 + 1);
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

        private bool CheckEmptyCeil()
        {
            return false;
        }

        private bool ValidateCoordInBounds()
        {
            return false;
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
        
        private void AddWorm()
        {
            string name = randomName.Generate(Sex.Male);
            _worms.Add(new Worm((0, 0), name));
        }
    }
}