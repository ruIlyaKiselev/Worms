using System.Threading.Tasks;
using ConsoleApp1.Network.Entity;
using Refit;

namespace ConsoleApp1.Network
{
    public interface INetworkService
    {
        [Post("/{wormName}/getAction")]
        Task<InfoFromServer> GetWormAction([AliasAs("wormName")] string wormName, [Body]InfoForServer infoForServer);
    }
}