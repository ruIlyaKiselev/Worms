using System;
using System.IO;
using System.Text;

namespace ConsoleApp1.Logging
{
    public class Logger
    {
        private string fileName;
        private IWorldInfoProvider infoProvider;
        public Logger(IWorldInfoProvider infoProvider)
        {
            this.infoProvider = infoProvider;
            fileName = GenerateLogFileName();
        }
        
        public void LogNewEvent()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder
                .Append(GenerateWormsLog())
                .Append(", ")
                .Append(GenerateFoodLog())
                .Append('\n');
            
            File.AppendAllText(fileName, stringBuilder.ToString());
            Console.WriteLine(stringBuilder.ToString());
        }

        /*
         * Генератор имени лог-файла.
         * Создается на основе текущего времени и даты,
         * что гарантирует уникальность имени в рамках одного компьютера
         */
        private string GenerateLogFileName()
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
                .ToString();
        }

        /*
         * метод генерирует для лог-файла для текущей итерации строку с информацией о червячках в виде имя-hp(coords)
         */
        private string GenerateWormsLog()
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
        private string GenerateFoodLog()
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