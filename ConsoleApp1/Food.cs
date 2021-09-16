namespace ConsoleApp1
{
    public class Food
    {
        private int _health = 10;
        private bool _isDeath = false;
        private (int, int) _currentPosition;
        
        public Food((int, int) currentPosition)
        {
            _currentPosition = currentPosition;
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
        
        public (int, int) CurrentPosition
        {
            get => _currentPosition;
            set
            {
                _currentPosition = value;
            }
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