using System;
using Slackers.Services.Repository;

namespace ProjectManager.Models
{
    public class Project : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int TaskCount { get; set; }
    }
}