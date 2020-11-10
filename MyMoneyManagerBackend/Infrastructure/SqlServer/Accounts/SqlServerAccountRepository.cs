using System;
using System.Collections.Generic;
using System.Data;
using Application.Repositories;


namespace Infrastructure.SqlServer.Accounts
{
    public class SqlServerAccountRepository: IAccountRepository
    {
        private IAccountFactory _accountFactory = new AccountFactory();
        public static readonly string TableName = "accounts";
        public static readonly string ColumnId = "account_id";
        public static readonly string ColumnBalance = "balance";
        public static readonly string ReqGet = $@"SELECT * FROM {TableName} WHERE {ColumnId}=@{ColumnId}";
        public static readonly string ReqCreate = $@"INSERT INTO {TableName} ({ColumnId},{ColumnBalance}) VALUES (@{ColumnId},@{ColumnBalance})";
        private double _defaultBalance = 500;

        public IEnumerable<IAccount> Query()
        {
            throw new NotImplementedException();
        }

        public IAccount Get(Guid id)
        {
            IAccount res = null;
            using(var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = ReqGet;
                command.Parameters.AddWithValue($"@{ColumnId}", id);
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    res = _accountFactory.CreateFromReader(reader);
                }
            }
            return res;
        }

        public IAccount Create(IUser user)
        {
            IAccount res = null;
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = ReqCreate;
                command.Parameters.AddWithValue($"@{ColumnId}", user.Id);
                command.Parameters.AddWithValue($"@{ColumnId}", _defaultBalance);
                command.ExecuteNonQuery();
                res = new Account()
                {
                    Id = user.Id,
                    Balance = _defaultBalance
                };
            }
            return res;
        }

        public bool Update(Guid id, IAccount account)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}