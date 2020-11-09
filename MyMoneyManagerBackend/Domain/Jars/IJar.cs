using System;
using Domain.Shared;

namespace Domain.Jars
{
    public interface IJar: IEntity
    {
        Guid Owner { get; set; }
        string Description { get; set; }
        string Name { get; set; }
        double Max { get; set; }
        double Balance { get; set; }
    }
}