using System.Data.SqlClient;
using Domain.Jars;
using Domain.Users;
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
                Owner = new User {Id = reader.GetGuid(reader.GetOrdinal(JarSqlServer.ColumnOwner))},
                Description = reader.GetString(reader.GetOrdinal(JarSqlServer.ColumnDescription)),
                Name = reader.GetString(reader.GetOrdinal(JarSqlServer.ColumnName)),
                Max = reader.GetDouble(reader.GetOrdinal(JarSqlServer.ColumnMax)),
                Balance =  reader.GetDouble(reader.GetOrdinal(JarSqlServer.ColumnBalance))
            };
        }

        public IJar CreateFromReaderWithAccount(SqlDataReader reader)
        {
            throw new System.NotImplementedException();
        }
    }
}