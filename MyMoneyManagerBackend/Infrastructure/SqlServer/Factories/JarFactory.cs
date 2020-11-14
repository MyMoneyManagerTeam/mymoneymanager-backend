using System.Data.SqlClient;
using Domain.Jars;
using Infrastructure.SqlServer.Jars;

namespace Infrastructure.SqlServer.Factories
{
    public class JarFactory: IInstanceFromReaderFactory<IJar>
    {
        public IJar CreateFromReader(SqlDataReader reader)
        {
            return new Jar()
            {
                Id = reader.GetGuid(reader.GetOrdinal(JarSqlServer.ColumnId)),
                Owner = reader.GetGuid(reader.GetOrdinal(JarSqlServer.ColumnOwner)),
                Description = reader.GetString(reader.GetOrdinal(JarSqlServer.ColumnDescription)),
                Name = reader.GetString(reader.GetOrdinal(JarSqlServer.ColumnName)),
                Max = reader.GetDouble(reader.GetOrdinal(JarSqlServer.ColumnMax)),
                Balance =  reader.GetDouble(reader.GetOrdinal(JarSqlServer.ColumnBalance))
            };
        }
    }
}