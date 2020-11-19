using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Application.Repositories;
using Domain.Jars;
using Domain.Users;
using Infrastructure.SqlServer.Factories;
using JarFactory = Infrastructure.SqlServer.Factories.JarFactory;

namespace Infrastructure.SqlServer.Jars
{
    public class JarRepository: IJarRepository
    {
        private IInstanceFromReaderFactory<IJar> _jarFactory = new JarFactory();

        public IEnumerable<IJar> Query(Guid userId)
        {
            IList<IJar> jars = new List<IJar>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = JarSqlServer.ReqQuery;
                cmd.Parameters.AddWithValue($"@{JarSqlServer.ColumnOwner}",userId);
                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    jars.Add(_jarFactory.CreateFromReader(reader));
                }
            }
            
            return jars;
            
        }

        public IJar Get(Guid userId, Guid jarId) 
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = JarSqlServer.ReqGet;
                cmd.Parameters.AddWithValue($"@{JarSqlServer.ColumnId}", jarId);
                cmd.Parameters.AddWithValue($"@{JarSqlServer.ColumnOwner}", userId);
                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                IJar jar = null;
                while (reader.Read())
                {
                    jar = _jarFactory.CreateFromReader(reader);
                }

                return jar;
            }
        }

        public IJar Create(IJar jar)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = JarSqlServer.ReqCreate;
                cmd.Parameters.AddWithValue($"@{JarSqlServer.ColumnOwner}",jar.Owner);
                cmd.Parameters.AddWithValue($"@{JarSqlServer.ColumnDescription}",jar.Description);
                cmd.Parameters.AddWithValue($"@{JarSqlServer.ColumnBalance}",jar.Balance);
                cmd.Parameters.AddWithValue($"@{JarSqlServer.ColumnMax}",jar.Max);
                cmd.Parameters.AddWithValue($"@{JarSqlServer.ColumnName}",jar.Name);
                
                jar.Id = (Guid) cmd.ExecuteScalar();
            }

            return jar;
        }

        public bool Update(Guid id, IJar jar)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = JarSqlServer.ReqPut;
                cmd.Parameters.AddWithValue($"@{JarSqlServer.ColumnOwner}",jar.Owner);
                cmd.Parameters.AddWithValue($"@{JarSqlServer.ColumnDescription}",jar.Description);
                cmd.Parameters.AddWithValue($"@{JarSqlServer.ColumnBalance}",jar.Balance);
                cmd.Parameters.AddWithValue($"@{JarSqlServer.ColumnName}",jar.Name);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(Guid userId, Guid jarId)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = JarSqlServer.ReqDelete;
                cmd.Parameters.AddWithValue($"@{JarSqlServer.ColumnOwner}",userId);
                cmd.Parameters.AddWithValue($"@{JarSqlServer.ColumnId}",jarId);
                
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public double TotalBalanceByUserId(Guid userId)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = JarSqlServer.ReqSumTotalBalance;
                cmd.Parameters.AddWithValue($"@{JarSqlServer.ColumnOwner}",userId);
                var res =  cmd.ExecuteScalar();
                if (res is System.DBNull)
                {
                    return 0;
                }
                return (double) cmd.ExecuteScalar();
            }
        }
    }
}