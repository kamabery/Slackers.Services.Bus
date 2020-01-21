using System.Threading.Tasks;
using MassTransit;

namespace Slackers.Services.Bus.MassTran.RabbitMQ
{
    public class TransitConsumer<T> : IConsumer<T> where T : EventMessage
    {
        private readonly IEventHandler<T> _handler;

        public TransitConsumer(IEventHandler<T> handler)
        {
            _handler = handler;
        }

        public async Task Consume(ConsumeContext<T> context)
        {
            await Task.Run(() => _handler.HandleEvent(context.Message));
        }
    }
}