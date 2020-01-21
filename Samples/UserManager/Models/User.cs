using System;
using Slackers.Services.Repository;

namespace UserManager.Models
{
    public class User : IEntity
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int TaskCount { get; set; }
    }
}