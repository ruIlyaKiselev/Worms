namespace ConsoleApp1
{
    /*
     * класс для описания пропертей еды
     */
    public class Food: IFoodInfoProvider
    {
        /*
         * конструктор
         * для создания еды необходимо передать ей её координаты, которые сгенерированы в генераторе
         */
        public Food((int, int) currentPosition)
        {
            Health = 10;
            IsDeath = false;
            CurrentPosition = currentPosition;
        }

        public int Health { get; private set; }

        public (int, int) CurrentPosition { get; set; }

        public bool IsDeath { get; private set; }

        /*
         * метод для уменьшения числа ходов, через которые еда "протухнет"; когда ходы закончатся, еда помечается через
         * boolean проперти как мертвая, после чего симулятор (игровой контроллер) её уничтожит уже извне
         */
        public bool DecreaseHealth()
        {
            Health -= 1;
            if (Health <= 0)
            {
                IsDeath = true;
            }

            return IsDeath;
        }

        public (int, int) ProvidePosition()
        {
            return CurrentPosition;
        }

        public int ProvideHealth()
        {
            return Health;
        }
    }
}