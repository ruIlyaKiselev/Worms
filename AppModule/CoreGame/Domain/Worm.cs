using ConsoleApp1.CoreGame.Enums;
using ConsoleApp1.CoreGame.Interfaces;

namespace ConsoleApp1.CoreGame.Domain
{
    /// <summary>
    ///     Класс <c>Worm</c> для описания червя. Имплементирует интерфейс <c>IWormInfoProvider</c>
    /// </summary>
    public class Worm: IWormInfoProvider
    {
        private IWormLogic _wormLogic;

        /// <summary>
        ///     Конструктор <c>Worm</c> принимает позицию, где червь появится, уникальное имя червя и логику действий червя.
        /// </summary>
        /// <param name="currentPosition">
        ///     Пара (int, int) - координаты червя по X и по Y в мире.
        /// </param>
        /// <param name="name">
        ///     Строка, хранящая уникальное имя червя в мире.
        /// </param>
        /// <param name="wormLogic">
        ///     Имплементация интерефейса IWormLogic, по которой червь будет принимать решение о своем действии.
        ///     Параметр опциональный (можно передать null), так как решение может приниматься извне
        ///     (классом World, где поведение берется из сети).
        /// </param>
        public Worm(
            (int, int) currentPosition, 
            string name,
            IWormLogic wormLogic)
            
        {
            Health = GameContract.StartWormHealth;
            ActionsIntent = Actions.None;
            DirectionIntent = Directions.None;
            CurrentPosition = currentPosition;
            Name = name;
            _wormLogic = wormLogic;
        }

        /// <value>Property <c>Health</c> хранит в себе очки здоровья данного червя.</value>
        public int Health { get; set; }
        /// <value>Property <c>CurrentPosition</c> хранит в себе координаты в мире данного червя.</value>
        public (int, int) CurrentPosition { get; set; }
        /// <value>Property <c>IsDeath</c> хранит в себе информацию, мертв ли червь (true - если мертв).</value>
        public bool IsDeath { get; set; }
        /// <value>Property <c>ActionsIntent</c> хранит в себе намерение на действие данного червя.</value>
        public Actions ActionsIntent { get; set; } 
        /// <value>Property <c>DirectionIntent</c> хранит в себе намерение на направление действия данного червя.</value>
        public Directions DirectionIntent { get; set; }
        /// <value>Property <c>Name</c> хранит в себе имя данного червя.</value>
        public string Name { get; set; }
        
        /// <summary>
        ///     Метод <c>GetIntent()</c> служит для получения намерения на действие червяка.
        ///     Возвращает пару <c>Actions</c> и <c>Directions</c>, то есть действие
        ///     (двигаться/размножаться/ничего не делать) и направление
        ///     (вверх/вниз/влево/вправо/никуда).
        /// </summary>
        /// <param name="infoProvider">
        ///     Интерфейс IWorldInfoProvider, служит для получения информации о мире,
        ///     то есть информации о всех червях, о всей еде и о текущей итерации.
        /// </param>
        /// <returns>
        ///     Возвращает пару (<c>Actions</c>, <c>Directions</c>),
        ///     где <c>Actions</c> - действие, <c>Directions</c> - направление действия.
        /// </returns>
        public (Actions, Directions) GetIntent(IWorldInfoProvider infoProvider)
        {
            var decidedIntent = _wormLogic.Decide(this, infoProvider);
            ActionsIntent = decidedIntent.Item1;
            DirectionIntent = decidedIntent.Item2;
            
            return (ActionsIntent, DirectionIntent);
        }
        
        /// <summary>
        ///     Метод для декремента здоровья после хода игры.
        /// </summary>
        ///  <returns>
        ///     Возвращает true, если червь мертв, иначе false.
        /// </returns>
        public bool DecreaseHealth()
        {
            Health -= GameContract.HealthDecreasePerIteration;
            if (Health <= 0)
            {
                IsDeath = true;
            }

            return IsDeath;
        }

        public string ProvideName()
        {
            return Name;
        }

        public (int, int) ProvidePosition()
        {
            return CurrentPosition;
        }

        public int ProvideHealth()
        {
            return Health;
        }

        /// <summary>
        ///     Метод для удобного получения координат рядом с червем (сверху/снизу/слева/справа).
        /// </summary>
        /// <param name="direction">
        ///     Направление из Enum <c>Directions</c>
        /// </param>
        /// <returns>
        ///     Возвращает пару (int, int) - координаты X и Y возле червя по переданному направлению.
        /// </returns>
        public (int, int) GetAroundPosition(Directions direction)
        {
            return direction switch
            {
                Directions.Bottom => (CurrentPosition.Item1, CurrentPosition.Item2 - 1),
                Directions.Top => (CurrentPosition.Item1, CurrentPosition.Item2 + 1),
                Directions.Left => (CurrentPosition.Item1 - 1, CurrentPosition.Item2),
                Directions.Right => (CurrentPosition.Item1 + 1, CurrentPosition.Item2),
                _ => CurrentPosition
            };
        }
        /// <summary>
        ///     Метод для удобного перемещения червя по координатам согласно переданному направлению.
        /// </summary>
        /// <param name="direction">
        ///     Направление из Enum <c>Directions</c>
        /// </param>
        /// <param name="infoProvider">
        ///     Интерфейс IWorldInfoProvider, служит для получения информации о мире,
        ///     то есть информации о всех червях, о всей еде и о текущей итерации.
        /// </param>
        public void Move(Directions direction, IWorldInfoProvider infoProvider)
        {
            switch (direction)
            {
                case Directions.Bottom:
                {
                    CurrentPosition = (CurrentPosition.Item1, CurrentPosition.Item2 - 1);
                    break;
                }
                case Directions.Top:
                {
                    CurrentPosition = (CurrentPosition.Item1, CurrentPosition.Item2 + 1);
                    break;
                }
                case Directions.Left:
                {
                    CurrentPosition = (CurrentPosition.Item1 - 1, CurrentPosition.Item2);
                    break;
                }
                case Directions.Right:
                {
                    CurrentPosition = (CurrentPosition.Item1 + 1, CurrentPosition.Item2);
                    break;
                }
            }
        }
    }
}