using ConsoleApp1.CoreGame.Interfaces;

namespace ConsoleApp1.CoreGame.Domain
{
    /// <summary>
    ///     Класс <c>Food</c> для описания еды. Имплементирует интерфейс <c>IFoodInfoProvider</c>
    /// </summary>
    public class Food: IFoodInfoProvider
    {
        /// <summary>
        ///     Конструктор <c>Food</c> принимает позицию, где еда появится.
        /// </summary>
        /// <param name="currentPosition">
        ///     Пара (int, int) - координаты червя по X и по Y в мире.
        /// </param>
        public Food((int, int) currentPosition)
        {
            Health = GameContract.StartFoodHealth;
            IsDeath = false;
            CurrentPosition = currentPosition;
        }

        /// <value>Property <c>Health</c> хранит в себе очки здоровья данной еды.</value>
        public int Health { get; set; }
        /// <value>Property <c>CurrentPosition</c> хранит в себе координаты в мире данной еды.</value>
        public (int, int) CurrentPosition { get; set; }
        /// <value>Property <c>IsDeath</c> хранит в себе информацию, испорчена ли еда (true - если испорчена).</value>
        public bool IsDeath { get; set; }

        /// <summary>
        ///     Метод для декремента здоровья после хода игры.
        /// </summary>
        ///  <returns>
        ///     Возвращает true, если еда испортилась, иначе false.
        /// </returns>
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