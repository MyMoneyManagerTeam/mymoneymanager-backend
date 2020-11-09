using System;

namespace Domain.Shared
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}