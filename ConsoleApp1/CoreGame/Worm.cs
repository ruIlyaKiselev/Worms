using System;
using System.Collections.Generic;
using ConsoleApp1.WormsLogic;

namespace ConsoleApp1
{
    public class Worm
    {
        private int _health = 10;
        private bool _isDeath = false;
        private Directions _directionsIntent = Directions.None;
        private Actions _actionsIntent = Actions.None;
        private IWormLogic _wormLogic;
        private (int, int) _currentPosition;
        private string _name;

        public Worm((int, int) currentPosition, string name)
        {
            _currentPosition = currentPosition;
            _name = name;
            _wormLogic = new FoodFindLogic();
        }
        
        public int Health
        {
            get => _health;
            set 
            {
                if (value > 0)
                {
                    _health = value;
                }
            }
        }

        public Actions ActionsIntent
        {
            get => _actionsIntent;
            set
            {
                _actionsIntent = value;
            }
        }
        
        public Directions DirectionIntent
        {
            get => _directionsIntent;
            set
            {
                _directionsIntent = value;
            }
        }

        public (int, int) CurrentPosition
        {
            get => _currentPosition;
            set
            {
                _currentPosition = value;
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
            }
        }

        public (Actions, Directions) GetIntent(GameField gameField)
        {
            _wormLogic.Decide(this, gameField);
            Console.WriteLine(_actionsIntent + " " + _directionsIntent);
            return (_actionsIntent, _directionsIntent);
        }

        public bool IsDeath()
        {
            return _isDeath;
        }
        
        public bool DecreaseHealth()
        {
            if (--_health <= 0)
            {
                _isDeath = true;
            }

            return _isDeath;
        }
    }
}