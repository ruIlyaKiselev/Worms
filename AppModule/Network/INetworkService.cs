using System.Threading.Tasks;
using ConsoleApp1.Network.Entity;
using Refit;

namespace ConsoleApp1.Network
{
    /// <summary>
    ///     Интерфейс-сервис для API запросов
    /// </summary>
    public interface INetworkService
    {
        /// <summary>
        ///     API вызов POST.
        ///     В качестве Alias wormName передается имя червя, для которого ожидается получить действие червя от сервера.
        ///     Внимание! API чувствителен к регистру, то есть Jonh и john - это разные имена.
        ///     В теле запроса хранится распарсенный в Json класс InfoForServer, содержащий текущую итерацию,
        ///     информацию о червях и о еде.
        ///     <code>
        /// Пример Json в body.
        /// BASE_URL = https://localhost:5001/api
        /// HOST = localhost
        /// PORT = 5001
        /// Обращение к API по адресу:
        /// https://localhost:5001/api/John/getAction
        /// {
        ///   "iteration": 0,
        ///   "worms": [
        ///     {
        ///       "name": "John",
        ///       "lifeStrength": 20,
        ///       "position": {
        ///         "x": 1,
        ///         "y": 1
        ///       }
        ///     }
        ///   ],
        ///   "food": [
        ///     {
        ///       "expiresIn": 10,
        ///       "position": {
        ///         "x": 2,
        ///         "y": -1
        ///       }
        ///     }
        ///   ]
        /// }
        ///     </code>
        /// </summary>
        /// <returns>
        ///     Возвращает Task с ответом сервера InfoFromServer.
        /// </returns>
        [Post("/{wormName}/getAction")]
        Task<InfoFromServer> GetWormAction([AliasAs("wormName")] string wormName, [Body]InfoForServer infoForServer);
    }
}