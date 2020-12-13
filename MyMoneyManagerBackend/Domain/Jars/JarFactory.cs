using System;
using System.Data.SqlClient;
using Domain.Jars;
using Domain.Users;

namespace Domain.Jars
{
    public class JarFactory: IJarFactory
    {
        public IJar GetFromParam(User owner, string description, string name, double max, double balance)
        {
            return new Jar
            {
                Owner = owner,
                Balance = balance,
                Description = description,
                Max = max,
                Name = name
            };
        }
    }
}