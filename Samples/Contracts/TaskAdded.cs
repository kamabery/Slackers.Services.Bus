using System;
using Slackers.Services.Bus;

namespace Contracts
{
    public class TaskAdded : EventMessage
    {
        public Guid TaskId { get; set; }

        public Guid ProjectId { get; set; }

        public Guid UserId { get; set; }
    }
}