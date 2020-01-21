using System;
using Slackers.Services.Repository;

namespace TaskManager.Models
{
    public class WorkTask : IEntity
    {
        public Guid UserId { get; set; }

        public Guid ProjectId { get; set; }

        public string Name { get; set; }

        public Guid Id { get; set; }
    }
}