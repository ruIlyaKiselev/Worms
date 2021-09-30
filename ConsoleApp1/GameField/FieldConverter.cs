namespace ConsoleApp1
{
    /*
     * Конвертер координат для трансляции реальных координат
     * (двумерное пространство X0Y, где x, y принадлежат целым числам)
     * в пространство zero-based массива, в виде которого сохранена карта.
     */
    public static class FieldConverter
    {
        /*
         *  Из пространства реальных координат в zero-based
         */
        public static (int, int) ToZeroBased((int, int) coord)
        {
            return (coord.Item1 - GameContract.StartX, -coord.Item2 + GameContract.FinishY);
        }

        /*
         *  Из пространства zero-based координат в реальные
         */
        public static (int, int) FromZeroBased((int, int) coord)
        {
            return (coord.Item1 + GameContract.StartX, -coord.Item2 + GameContract.FinishY);
        }
        
        /*
         * метод для удобного человекочитаемого отображения объекта на игровом поле в его символ для вывода в консоль
         */
        public static string FieldObjectConverter(FieldObjects fieldObjects)
        {
            switch (fieldObjects)
            {
                case FieldObjects.Empty: return "E";
                case FieldObjects.Worm: return "W";
                case FieldObjects.Food: return "F";
                default: return "E";
            }
        }
    }
}