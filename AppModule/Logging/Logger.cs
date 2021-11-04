using System;
using System.IO;
using System.Text;

namespace ConsoleApp1.Logging
{
    public class Logger: ILogger
    {
        private string fileName;
        private IWorldInfoProvider storedInfoProvider;
        public Logger(IWorldInfoProvider storedInfoProvider)
        {
            this.storedInfoProvider = storedInfoProvider;
            fileName = GenerateLogFileName();
        }
        
        public Logger()
        {
            fileName = GenerateLogFileName();
        }

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

        public void LogNewEvent()
        {
            if (storedInfoProvider == null)
            {
                throw new ArgumentException("Info provider should be initialized in constructor");
            }

            LogNewEvent(storedInfoProvider);
        }

        /*
         * Генератор имени лог-файла.
         * Создается на основе текущего времени и даты,
         * что гарантирует уникальность имени в рамках одного компьютера
         */
        private static string GenerateLogFileName()
        {
            var currentDate = DateTime.Now;

            return new StringBuilder()
                .Append("GameSession")
                .Append('_')
                .Append(currentDate.Hour)
                .Append('_')
                .Append(currentDate.Minute)
                .Append('_')
                .Append(currentDate.Second)
                .Append('_')
                .Append(currentDate.Day)
                .Append('_')
                .Append(currentDate.Month)
                .Append('_')
                .Append(currentDate.Year)
                .Append(".txt")
                .ToString();
        }

        /*
         * метод генерирует для лог-файла для текущей итерации строку с информацией о червячках в виде имя-hp(coords)
         */
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

        /*
         * метод генерирует для лог-файла для текущей итерации строку с информацией о еде в виде hp(coords)
         */
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