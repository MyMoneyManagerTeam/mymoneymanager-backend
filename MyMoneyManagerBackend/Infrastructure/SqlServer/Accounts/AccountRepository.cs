using System;
using System.Collections.Generic;
using System.Data;
using Application.Repositories;
using Domain.Accounts;
using Domain.Users;
using Infrastructure.SqlServer.Factories;
using AccountFactory = Infrastructure.SqlServer.Factories.AccountFactory;


namespace Infrastructure.SqlServer.Accounts
{
    public class AccountRepository: IAccountRepository
    {
        private IInstanceFromReaderFactory<IAccount> _accountFactory = new AccountFactory();

        public IAccount Get(Guid id)
        {
            IAccount res = null;
            using(var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = AccountSqlServer.ReqGet;
                command.Parameters.AddWithValue($"@{AccountSqlServer.ColumnId}", id);
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    res = _accountFactory.CreateFromReader(reader);
                }
            }
            return res;
        }

        public IAccount Create(Guid userId)
        {
            IAccount res = null;
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = AccountSqlServer.ReqCreate;
                command.Parameters.AddWithValue($"@{AccountSqlServer.ColumnId}", userId);
                command.Parameters.AddWithValue($"@{AccountSqlServer.ColumnBalance}", AccountSqlServer._defaultBalance);
                command.ExecuteNonQuery();
                res = new Account()
                {
                    Id = userId,
                    Balance = AccountSqlServer._defaultBalance
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

        public bool ModifyBalance(Guid id, double amount)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = AccountSqlServer.ReqModifyBalance;
                command.Parameters.AddWithValue($"@{AccountSqlServer.ColumnId}", id);
                command.Parameters.AddWithValue($"@{AccountSqlServer.ColumnBalance}", amount);
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}