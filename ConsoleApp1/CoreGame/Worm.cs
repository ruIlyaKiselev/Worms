using System;
using System.Collections.Generic;
using ConsoleApp1.WormsLogic;

namespace ConsoleApp1
{
    public class Worm
    {
        private int _health;
        private bool _isDeath;
        private Directions _directionsIntent = Directions.None;
        private Actions _actionsIntent = Actions.None;
        private IWormLogic _wormLogic;
        private (int, int) _currentPosition;
        private string _name;

        public Worm((int, int) currentPosition, string name)
        {
            _health = 10;
            _currentPosition = currentPosition;
            _name = name;
            _wormLogic = new FoodFindLogic();
        }
        
        public int Health { get; set; }
        public Actions ActionsIntent { get; set; }
        public Directions DirectionIntent { get; set; }
        public (int, int) CurrentPosition { get; set; }

        public string Name { get; set; }

        public (Actions, Directions) GetIntent(GameField gameField)
        {
            _wormLogic.Decide(this, gameField);
            Console.WriteLine(_actionsIntent + " " + _directionsIntent);
            return (_actionsIntent, _directionsIntent);
        }

        public bool IsDeath { get; set; }
        
        public bool DecreaseHealth()
        {
            _health -= 1;
            if (_health <= 0)
            {
                _isDeath = true;
            }

            return _isDeath;
        }
    }
}