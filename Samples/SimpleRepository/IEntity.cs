using System;

namespace SimpleRepository
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}