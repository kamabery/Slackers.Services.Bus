using System.Threading.Tasks;

namespace Slackers.Services.Bus
{
    public interface IEventBus
    {
        Task<bool> Publish<T>(T eventMessage) where T : EventMessage;
    }
}