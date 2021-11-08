using System;

namespace ConsoleApp1.CoreGame.Exceptions
{
    /// <summary>
    ///     Исключение для ситуации, когда новый червь добавляется не на пустую координату, что не соответствует правилам.
    /// </summary>
    public class AddingWormException: ArgumentException
    {
        
    }
}