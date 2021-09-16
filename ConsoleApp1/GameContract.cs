using System.ComponentModel;

namespace ConsoleApp1
{
    public static class GameContract
    {
        // Game field properties 
        public static readonly int StartX = -2;
        public static readonly int FinishX = 2;
        public static readonly int StartY = -2;
        public static readonly int FinishY = 2;
        
        public static readonly int Height = FinishX - StartX + 1;
        public static readonly int Width = FinishY - StartY + 1;
        public static readonly int NumberOfSteps = 100;

        // Gaussian distribution properties
        public static readonly double Mu = 0;
        public static readonly double Sigma = 5;
        
        // Game field object's types
        public enum FieldObjects
        {
            Empty,
            Worm,
            Food
        }

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