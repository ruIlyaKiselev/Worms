using System;
using System.IO;
using System.Text;
using ConsoleApp1.CoreGame.Interfaces;

namespace ConsoleApp1.Logging
{
    /// <summary>
    ///     Класс для логирования. Имплементирует интерфейс ILogger
    /// </summary>
    public class Logger: ILogger
    {
        private string fileName;
        private IWorldInfoProvider storedInfoProvider;
        
        /// <summary>
        ///     Конструктор для создания Logger.
        ///     Запоминает мир единожны, с этим конструктором можно использовать метод LogNewEvent() без параметров.
        /// </summary>
        /// <param name="storedInfoProvider">
        ///     Интерфейс информации о мире IWorldInfoProvider
        /// </param>
        public Logger(IWorldInfoProvider storedInfoProvider)
        {
            this.storedInfoProvider = storedInfoProvider;
            fileName = GenerateLogFileName();
        }
        
        /// <summary>
        ///     Конструктор для создания Logger.
        ///     Не запоминает информацию о мире, с
        ///     этим конструктором при каждом логировании необходимо LogNewEvent(IWorldInfoProvider infoProvider)
        /// </summary>
        /// <param name="storedInfoProvider">
        ///     Интерфейс информации о мире IWorldInfoProvider
        /// </param>
        public Logger()
        {
            fileName = GenerateLogFileName();
        }

        /// <summary>
        ///     Метод для логирования состояния мира по переданной информации из IWorldInfoProvider
        /// </summary>
        /// <param name="infoProvider">
        ///     Интерфейс информации о мире IWorldInfoProvider, по которому делаются логи
        /// </param>
        public void LogNewEvent(IWorldInfoProvider infoProvider)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder
                .Append(GenerateWormsLog(infoProvider))
                .Append(", ")
                .Append(GenerateFoodLog(infoProvider))
                .Append('\n');
            
            File.AppendAllText(fileName, stringBuilder.ToString());
            Console.WriteLine(stringBuilder.ToString());
        }

        /// <summary>
        ///     Метод для логирования состояния мира по состоянию мира, которое было передано в конструктор класса
        /// </summary>
        
        public void LogNewEvent()
        {
            if (storedInfoProvider == null)
            {
                throw new ArgumentException("Info provider should be initialized in constructor");
            }

            LogNewEvent(storedInfoProvider);
        }

        /// <summary>
        ///     Метод-генератор имени лог-файла.
        ///     Создается на основе текущего времени и даты,
        ///     что гарантирует уникальность имени в рамках одного компьютера
        /// </summary>
        /// <returns>
        ///     Возвращает строку формата hh_mm_ss-dd_MM_YYYY.txt
        /// </returns>
        private static string GenerateLogFileName()
        {
            var currentDate = DateTime.Now;

            return $@"GameSession_{currentDate.Hour}_{currentDate.Minute}_{currentDate.Second}_{currentDate.Day}_{currentDate.Month}_{currentDate.Year}.txt";
        }

        /// <summary>
        ///     Метод генерирует для лог-файла для текущей итерации строку с информацией о червячках
        /// </summary>
        /// <param name="infoProvider">
        ///     Интерфейс информации о мире IWorldInfoProvider, по которому делаются логи
        /// </param>
        /// <returns>
        ///     Возвращает строку в виде имя-hp(coords)
        /// </returns>
        private static string GenerateWormsLog(IWorldInfoProvider infoProvider)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("Worms: [");

            foreach (var worm in infoProvider.ProvideWorms())
            {
                stringBuilder
                    .Append(worm.ProvideName())
                    .Append('-')
                    .Append(worm.ProvideHealth())
                    .Append(worm.ProvidePosition().ToString())
                    .Append(", ");
            }
            
            stringBuilder.Append(']');
            return stringBuilder.ToString();
        }

       
        /// <summary>
        ///     Метод генерирует для лог-файла для текущей итерации строку с информацией о еде
        /// </summary>
        /// <param name="infoProvider">
        ///     Интерфейс информации о мире IWorldInfoProvider, по которому делаются логи
        /// </param>
        /// <returns>
        ///     Возвращает строку в виде hp(coords)
        /// </returns>
        private static string GenerateFoodLog(IWorldInfoProvider infoProvider)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("Food: [");
            
            foreach (var food in infoProvider.ProvideFood())
            {
                
                stringBuilder
                    .Append(food.ProvideHealth())
                    .Append(food.ProvidePosition().ToString())
                    .Append(", ");
            }

            stringBuilder.Append(']');
            return stringBuilder.ToString();
        }
    }
}