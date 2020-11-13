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
                Id = reader.GetGuid(reader.GetOrdinal(SqlServerJarRepository.ColumnId)),
                Owner = reader.GetGuid(reader.GetOrdinal(SqlServerJarRepository.ColumnOwner)),
                Description = reader.GetString(reader.GetOrdinal(SqlServerJarRepository.ColumnDescription)),
                Name = reader.GetString(reader.GetOrdinal(SqlServerJarRepository.ColumnName)),
                Max = reader.GetDouble(reader.GetOrdinal(SqlServerJarRepository.ColumnMax)),
                Balance =  reader.GetDouble(reader.GetOrdinal(SqlServerJarRepository.ColumnBalance))
            };
        }
    }
}