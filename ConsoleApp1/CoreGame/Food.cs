namespace ConsoleApp1
{
    /*
     * класс для описания пропертей еды
     */
    public class Food
    {
        private int _health = 10; // сколько ходов еде осталось жить
        private bool _isDeath = false; // проперти чтобы удобно было проверять не "протухла" ли еда и удалять её

        /*
         * конструктор
         * для создания еды необходимо передать ей её координаты, которые сгенерированы в генераторе
         */
        public Food((int, int) currentPosition)
        {
            CurrentPosition = currentPosition;
        }
        
        public int Health { get; }
        public (int, int) CurrentPosition { get; }
        public bool IsDeath { get; }

        /*
         * метод для уменьшения числа ходов, через которые еда "протухнет"; когда ходы закончатся, еда помечается через
         * boolean проперти как мертвая, после чего симулятор (игровой контроллер) её уничтожит уже извне
         */
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