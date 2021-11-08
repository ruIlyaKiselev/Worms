using System;

namespace ConsoleApp1.CoreGame.Exceptions
{
    /// <summary>
    ///     Исключение для ситуации, когда червь перемещается на клетку, где находится другой червь, что не соответствует правилам.
    /// </summary>
    public class WormMovementException: Exception
    {
        
    }
}