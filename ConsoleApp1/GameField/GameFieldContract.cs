namespace ConsoleApp1
{
    public static class GameFieldContract
    {
        public static readonly int StartX = -4;
        public static readonly int FinishX = 4;
        public static readonly int StartY = -4;
        public static readonly int FinishY = 4;
        
        public static readonly int Height = FinishX - StartX + 1;
        public static readonly int Width = FinishY - StartY + 1;
    }
}