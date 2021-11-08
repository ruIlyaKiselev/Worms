using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.CoreGame.Enums;
using ConsoleApp1.CoreGame.Exceptions;
using ConsoleApp1.CoreGame.Interfaces;
using ConsoleApp1.Generators;
using ConsoleApp1.Logging;
using ConsoleApp1.Repository;

namespace ConsoleApp1.CoreGame.Domain
{
    /// <summary>
    ///     Класс <c>World</c> для описания мира. Имплементирует интерфейс <c>IWorldInfoProvider</c>
    ///     Хранит в себе в виде листов всех червей и всю еду, текущий ход игры.
    /// </summary>
    public class World: IWorldInfoProvider
    {
        private readonly List<Worm> _worms = new();
        private readonly List<Food> _food = new();
        private int _gameIterationCounter = 0;
        
        private IFoodGenerator _foodGenerator;
        private INameGenerator _nameGenerator;
        private IWormLogic _wormLogic;
        private ILogger _logger;
        private IRepository _repository;

        /// <summary>
        ///     Конструктор <c>World</c> принимает генераторы еды и имён, логику червей,
        ///     по которой мир будет принимать решение для каждого червя по умолчанию (без решений, полученных с API),
        ///     логгер и репозиторий (прослойка между БД и сетевой частью).
        /// </summary>
        /// <param name="foodGenerator">
        ///     Имплементация интерфейса <c>IFoodGenerator</c>, служит для генерации еды с уникальными координатами.
        /// </param>
        /// <param name="nameGenerator">
        ///     Имплементация интерфейса <c>INameGenerator</c>, служит для генерации уникальных имён.
        /// </param>
        /// <param name="wormLogic">
        ///     Имплементация интерефейса <c>IWormLogic</c>, по которой мир для каждого червя по умолчанию будет принимать
        ///     решение о действии. Параметр опциональный (можно передать null), так как решение может приниматься по
        ///     данным из сети (берется из репозитория).
        /// </param>
        /// <param name="logger">
        ///     Имплементация интерефейса <c>ILogger</c>, служит для логирования состояния мира на каждому ходе.
        /// </param>
        /// <param name="repository">
        ///     Имплементация интерефейса <c>IRepository</c>, служит для взятия данных из сети или из БД.
        /// </param>
        public World(
            IFoodGenerator foodGenerator,
            INameGenerator nameGenerator,
            IWormLogic wormLogic,
            ILogger logger,
            IRepository repository
        ) 
        {
            _foodGenerator = foodGenerator;
            _nameGenerator = nameGenerator;
            _wormLogic = wormLogic;
            _logger = logger;
            _repository = repository;
        }
        
        public List<Worm> GetWorms()
        {
            return _worms;
        } 
        
        public List<Food> GetFood()
        {
            return _food;
        }

        /// <summary>
        ///     Метод для добавления червя в мир.
        ///     Если целевая клетка (координата червя) пустая (нет еды и другого червя),
        ///     то червь добавляется, иначе кидается исключение <c>AddingWormException</c>.
        /// </summary>
        /// <param name="worm">
        ///     Червь, который добавляется в мир. Должен быть полностью подготовлен
        ///     (иметь координату для добавления, уникальное имя и поведение).
        /// </param>
        /// <exception cref="AddingWormException"></exception>
        public void AddWorm(Worm worm)
        {
            if (CheckCeil(worm.CurrentPosition) == FieldObjects.Empty)
            {
                _worms.Add(worm);
            }
            else
            {
                throw new AddingWormException();
            }
        }

        /// <summary>
        ///     Метод для добавления еды в мир.
        ///     Если целевая клетка пустая (нет еды и другого червя),
        ///     то еда просто добавляется.
        ///     Если целевая клетка занята червем,
        ///     то этому червю прибавляется здоровье в соответствии с правилами и
        ///     еда не помещается на карту.
        ///     Во всех остальных случаях (целевая клетка занята едой)
        ///     кидается исключение <c>AddingFoodException</c>.
        /// </summary>
        /// <param name="food">
        ///     Еда, которая добавляется в мир. Должна быть полностью подготовлена
        ///     (иметь координату для добавления).
        /// </param>
        /// <exception cref="AddingFoodException"></exception>
        public void AddFood(Food food)
        {
            if (CheckCeil(food.CurrentPosition) == FieldObjects.Food)
            {
                throw new AddingFoodException();
            }
            
            if (CheckCeil(food.CurrentPosition) == FieldObjects.Empty)
            {
                _food.Add(food);
            }
            else
            {
                foreach (Worm worm in _worms)
                {
                    if (worm.CurrentPosition == food.CurrentPosition)
                    {
                        worm.Health += GameContract.FoodSaturation;
                        return;
                    }
                }
            }
        }
        /// <summary>
        ///     Метод для проверки целевой клетки;
        ///     Пробегается по листу червей и по листу еды;
        ///     Если найден червь с совпадающей коордитатой, 
        ///     то возвращается FieldObjects.Worm;
        ///     Если найдена еда с совпадающей коордитатой, 
        ///     то возвращается FieldObjects.Food;
        ///     Иначе возвращается FieldObjects.Empty.
        /// </summary>
        /// <param name="ceilCoords">
        ///     Пара (int, int) - координата, для которой требуется выяснить тип клетки в мире.
        /// </param>
        /// <returns>
        ///     Возвращает значение из enum FieldObjects.
        ///     Если найден червь с совпадающей коордитатой, 
        ///     то возвращается FieldObjects.Worm;
        ///     Если найдена еда с совпадающей коордитатой, 
        ///     то возвращается FieldObjects.Food;
        ///     Иначе возвращается FieldObjects.Empty.
        /// </returns>
        private FieldObjects CheckCeil((int, int) ceilCoords)
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
        /// <summary>
        ///     Метод, убавляющий у всех объектов мира (еда и черви) здоровье;
        ///     Применяется в конце каждой итерации мира.
        /// </summary>
        public void DecreaseHealths()
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
        
        /// <summary>
        ///     Метод, инкрементирующий счетчик ходов
        /// </summary>
        public void IncrementIteration()
        {
            _gameIterationCounter++;
        }
        
        /// <summary>
        ///     Метод для передвижения червя.
        ///     Проверяет, что находится на клетке, на которую хочет перейти червь.
        ///     Если на целевой клетке другой червь, то бросается исключение <p>WormMovementException</p>,
        ///     иначе происходит перемещение на целевую клетку.
        ///     В конце производится проверка, есть ли еда на целевой клетке и в случае, если она есть, 
        ///     эта еда удаляется и у червя восполняется здоровье в соответствии с правилами.
        /// </summary>
        /// <param name="worm">
        ///     Червь, который размножается.
        /// </param>
        /// <param name="direction">
        ///     Направление из enum <p>Directions</p>, по которому будет выбираться целевая координата
        ///     на появление нового червя относительно координаты размножающегося червя.
        /// </param>
        /// <exception cref="WormMovementException"></exception>
        public void MoveWorm(Worm worm, Directions direction)
        {
            var futurePosition = worm.GetAroundPosition(direction);
            
            if (CheckCeil(futurePosition) == FieldObjects.Worm)
            {
                throw new WormMovementException();
            }
            
            worm.Move(direction, this);
            
            foreach (var food in _food.ToArray())
            {
                if (food.CurrentPosition == futurePosition)
                {
                    _food.Remove(food);
                    worm.Health += GameContract.FoodSaturation;
                }
            }
        }
        
        /// <summary>
        ///     Метод для размножения червя.
        ///     Проверяет, что находится на клетке, на которую хочет размножиться червь.
        ///     Если целевая клетка не пустая, то бросается исключение <p>WormBuddingException</p>,
        ///     иначе на целевой клетке появляется новый червь, а у размножающегося червя
        ///     отнимается здоровье в соответствии с правилами.
        /// </summary>
        /// <param name="worm">
        ///     Червь, который размножается.
        /// </param>
        /// <param name="direction">
        ///     Направление из enum <p>Directions</p>, по которому будет выбираться целевая координата
        ///     на появление нового червя относительно координаты размножающегося червя.
        /// </param>
        /// <exception cref="WormBuddingException"></exception>
        public void BudWorm(Worm worm, Directions direction)
        {
            var futurePosition = worm.GetAroundPosition(direction);
            
            if (CheckCeil(futurePosition) != FieldObjects.Empty)
            {
                throw new WormBuddingException();
            }

            worm.Health -= GameContract.WormBuddingDamage;
            AddWorm(new Worm(futurePosition, _nameGenerator.Generate(), _wormLogic));
        }
        
        /// <summary>
        ///     Метод для принятия решений всеми червями.
        ///     Делает так, чтобы каждый червь принял решение на перемещение, размножение или ничего не делание.
        ///     Когда решение принято, происходит проверка на возможность такого действия и на
        ///     соответствие его правилам и в случае, если всё по правилам, это действие происходит, то
        ///     есть червь либо размножается, либо перемещается, либо ничего не делает.
        ///
        ///     Если действие червя с API не доступно, то по таймауту решение червя принимается локально.
        /// </summary>
        public void DecideWormsIntents()
        {
            foreach (var worm in new List<Worm>(GetWorms()))
            {
                (Actions, Directions) wormIntent;

                try
                {
                    wormIntent = _repository.GetWormActionFromAPI(worm.Name, this);
                }
                catch
                {
                    wormIntent = _wormLogic.Decide(worm, this);
                }
                
                if (wormIntent.Item1 == Actions.Move)
                {
                    if (CheckCeil(worm.GetAroundPosition(wormIntent.Item2)) != FieldObjects.Worm)
                    {
                        MoveWorm(worm, wormIntent.Item2);
                    }
                }
                else if (wormIntent.Item1 == Actions.Budding)
                {
                    if (CheckCeil(worm.GetAroundPosition(wormIntent.Item2)) == FieldObjects.Empty)
                    {
                        BudWorm(worm, wormIntent.Item2);
                    }
                }
            }
        }

        public List<IWormInfoProvider> ProvideWorms()
        {
            return _worms.Cast<IWormInfoProvider>().ToList();
        }

        public List<IFoodInfoProvider> ProvideFood()
        {
            return _food.Cast<IFoodInfoProvider>().ToList();
        }

        public int ProvideGameIteration()
        {
            return _gameIterationCounter;
        }
    }
}