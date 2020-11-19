using System;
using System.Data.SqlClient;
using Domain.Jars;

namespace Domain.Jars
{
    public class JarFactory: IJarFactory
    {
        public IJar GetFromParam(Guid owner, string description, string name, double max, double balance)
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