using System;

namespace Slackers.Services.Bus
{
    public class EventMessage
    {
        public EventMessage()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }

        public DateTime CreationDate { get; private set; }
    }
}