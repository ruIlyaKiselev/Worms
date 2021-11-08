namespace ConsoleApp1.CoreGame
{
    /// <summary>
    ///     Класс для прокидывания параметров Hosted Service.
    /// </summary>
    public class IntegrationService
    {
        public IntegrationService(string wormBehaviorName)
        {
            WormBehaviorName = wormBehaviorName;
        }
        /// <value>Property <c>WormBehaviorName</c> - имя поведения мира, которое нужно передть сервису. </value>
        public string WormBehaviorName { get; set; }
    }
}