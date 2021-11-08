using System;

namespace ConsoleApp1.CoreGame.Exceptions
{
    /// <summary>
    ///     Исключение для ситуации, когда еда добавляется на координату другой еды, что не соответствует правилам.
    /// </summary>
    public class AddingFoodException: ArgumentException
    {
        
    }
}