using System.Threading.Tasks;
using ConsoleApp1.Network.Entity;
using Refit;

namespace ConsoleApp1.Network
{
    public interface NetworkService
    {
        [Post("{wormName})/getAction")]
        Task<InfoFromServer> GetWormAction(string user);
    }
}