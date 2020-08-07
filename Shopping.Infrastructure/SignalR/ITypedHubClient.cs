using System.Threading.Tasks;

namespace Shopping.Infrastructure.SignalR
{
    public interface ITypedHubClient
    {
        Task BroadcastMessage(string name, string message);
    }
}