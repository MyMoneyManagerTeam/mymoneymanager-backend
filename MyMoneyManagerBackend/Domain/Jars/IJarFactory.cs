using System;
using System.Data.SqlClient;
using Domain.Jars;
using Domain.Users;

namespace Domain.Jars
{
    public interface IJarFactory
    {
        public IJar GetFromParam(User owner, Guid id, string description, string name, double max, double balance);

    }
}