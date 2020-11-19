using System;
using System.Collections.Generic;
using System.Data;
using Application.Repositories;
using Domain.Transactions;
using Domain.Users;
using Infrastructure.SqlServer.Factories;
using TransactionFactory = Infrastructure.SqlServer.Factories.TransactionFactory;

namespace Infrastructure.SqlServer.Transactions
{
    public class TransactionRepository: ITransactionRepository
    {
        private IInstanceFromReaderFactory<ITransaction> _transactionFactory = new TransactionFactory();
        
        public IEnumerable<ITransaction> Query(Guid userId)
        {
            IList<ITransaction> res = new List<ITransaction>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = TransactionSqlServer.ReqQuery;
                command.Parameters.AddWithValue($"@{TransactionSqlServer.ColumnEmitterId}",userId);
                command.Parameters.AddWithValue($"@{TransactionSqlServer.ColumnReceiverId}",userId);
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    res.Add(_transactionFactory.CreateFromReader(reader));
                }
            }

            return res;
        }

        public ITransaction Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public ITransaction Create(ITransaction transaction)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = TransactionSqlServer.ReqCreate;
                command.Parameters.AddWithValue($"@{TransactionSqlServer.ColumnEmitterId}",transaction.EmitterId);
                command.Parameters.AddWithValue($"@{TransactionSqlServer.ColumnReceiverId}",transaction.ReceiverId);
                command.Parameters.AddWithValue($"@{TransactionSqlServer.ColumnAmount}",transaction.Amount);
                command.Parameters.AddWithValue($"@{TransactionSqlServer.ColumnTransactionDate}",transaction.TransactionDate);
                command.Parameters.AddWithValue($"@{TransactionSqlServer.ColumnDescription}",transaction.Description);
                command.Parameters.AddWithValue($"@{TransactionSqlServer.ColumnEmitterName}",transaction.EmitterName);
                command.Parameters.AddWithValue($"@{TransactionSqlServer.ColumnReceiverName}",transaction.ReceiverName);
                transaction.Id = (Guid) command.ExecuteScalar();
            }
            return transaction;
        }

        public bool Update(Guid id, ITransaction transaction)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}