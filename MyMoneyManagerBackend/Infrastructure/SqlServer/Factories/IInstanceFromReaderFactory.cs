using System.Data.SqlClient;

namespace Infrastructure.SqlServer.Factories
{
    public interface IInstanceFromReaderFactory <out T>
    {
        T CreateFromReader(SqlDataReader reader);
    }
}