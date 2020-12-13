using System;
using Domain.Shared;
using Domain.Users;

namespace Domain.Jars
{
    public interface IJar: IEntity
    {
        User Owner { get; set; }
        string Description { get; set; }
        string Name { get; set; }
        double Max { get; set; }
        double Balance { get; set; }
    }
}