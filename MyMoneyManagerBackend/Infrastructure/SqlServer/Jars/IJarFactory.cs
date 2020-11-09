using System.Data.SqlClient;
using Domain.Jars;

namespace Infrastructure.SqlServer.Jars
{
    public interface IJarFactory
    {
        IJar CreateFromReader(SqlDataReader reader);
    }
}