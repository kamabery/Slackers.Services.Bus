using System.Threading.Tasks;

namespace Slackers.Services.Bus
{
    public interface IEventHandler<T> where T : EventMessage
    {
        Task HandleEvent(T eventMessage);
    }
}