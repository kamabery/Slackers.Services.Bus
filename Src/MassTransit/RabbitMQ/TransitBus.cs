using System.Threading.Tasks;
using MassTransit;

namespace Slackers.Services.Bus.MassTran.RabbitMQ
{
    public class TransitBus : IEventBus
    {
        private readonly IBusControl _busControl;

        public TransitBus(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public async Task<bool> Publish<T>(T eventMessage) where T : EventMessage
        {
            await _busControl.Publish(eventMessage);
            return true;
        }
    }
}