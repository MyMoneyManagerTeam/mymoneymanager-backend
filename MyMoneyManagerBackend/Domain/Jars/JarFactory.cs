using System;
using System.Data.SqlClient;
using Domain.Jars;
using Domain.Users;

namespace Domain.Jars
{
    public class JarFactory: IJarFactory
    {
        public IJar GetFromParam(User owner, Guid id, string description, string name, double max, double balance)
        {
            return new Jar
            {
                Owner = owner,
                Id = id,
                Balance = balance,
                Description = description,
                Max = max,
                Name = name
            };
        }
    }
}