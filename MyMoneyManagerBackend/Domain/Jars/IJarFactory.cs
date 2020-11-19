using System;
using System.Data.SqlClient;
using Domain.Jars;

namespace Domain.Jars
{
    public interface IJarFactory
    {
        public IJar GetFromParam(Guid owner, string description, string name, double max, double balance);

    }
}