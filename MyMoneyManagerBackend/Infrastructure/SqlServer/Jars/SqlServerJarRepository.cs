using System;
using System.Collections.Generic;
using System.Data;
using Application.Repositories;
using Domain.Jars;
using Domain.Users;
using Infrastructure.SqlServer.Factories;
using JarFactory = Infrastructure.SqlServer.Factories.JarFactory;

namespace Infrastructure.SqlServer.Jars
{
    public class SqlServerJarRepository: IJarRepository
    {
        private IInstanceFromReaderFactory<IJar> _jarFactory = new JarFactory();
        public static readonly string TableName = "jars";
        public static readonly string ColumnId = "jar_id";
        public static readonly string ColumnOwner = "owner";
        public static readonly string ColumnDescription = "description";
        public static readonly string ColumnName = "name";
        public static readonly string ColumnMax = "max";
        public static readonly string ColumnBalance = "balance";
        public static readonly string ReqQuery = $@"SELECT * FROM {TableName} WHERE {ColumnOwner}=@{ColumnOwner}";
        public IEnumerable<IJar> Query(IUser user)
        {
            IList<IJar> res = new List<IJar>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = ReqQuery;
                command.Parameters.AddWithValue($"@{ColumnOwner}",user.Id);
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    res.Add(_jarFactory.CreateFromReader(reader));
                }
            }
            return res;
        }

        public IJar Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IJar Create(IJar jar)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid id, IJar jar)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}