using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

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
            File.AppendAllText(fileName, GenerateIterationLog());    
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

        private string GenerateIterationLog()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("Worms: [");
            
            foreach (var worm in infoProvider.ProvideWorms())
            {
                stringBuilder.Append(worm.Name);
                stringBuilder.Append('-');
                stringBuilder.Append(worm.Health);
                stringBuilder.Append(" (");
                stringBuilder.Append(worm.CurrentPosition.ToString());
                stringBuilder.Append(")]");
            }

            stringBuilder.Append('\n');

            return stringBuilder.ToString();
        }
    }
}