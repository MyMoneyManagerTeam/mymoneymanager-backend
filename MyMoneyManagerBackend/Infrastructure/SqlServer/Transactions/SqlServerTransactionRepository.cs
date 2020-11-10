using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using Application.Repositories;
using Domain.Transactions;
using Domain.Users;

namespace Infrastructure.SqlServer.Transactions
{
    public class SqlServerTransactionRepository: ITransactionRepository
    {
        private ITransactionFactory _transactionFactory = new TransactionFactory();
        public static readonly string TableName = "transactions";
        public static readonly string ColumnId = "transaction_id";
        public static readonly string ColumnEmitterId = "emitter_id";
        public static readonly string ColumnReceiverId = "receiver_id";
        public static readonly string ColumnAmount = "amount";
        public static readonly string ColumnTransactionDate = "transaction_date";
        public static readonly string ColumnDescription = "description";
        public static readonly string ColumnEmitterName = "emitter_name";
        public static readonly string ColumnReceiverName = "receiver_name";

        public static readonly string ReqQuery =
            $@"SELECT * FROM {TableName} 
            WHERE {ColumnEmitterId}=@{ColumnEmitterId} OR {ColumnReceiverId}=@{ColumnReceiverId}";

        public static readonly string ReqCreate = 
            $@"INSERT INTO {TableName} ({ColumnId},{ColumnEmitterId},{ColumnReceiverId},{ColumnAmount},{ColumnTransactionDate},{ColumnDescription},{ColumnEmitterName},{ColumnReceiverName}) 
            OUTPUT INSERTED.{ColumnId} 
            VALUES 
            (NEWID(),@{ColumnEmitterId},@{ColumnReceiverId},@{ColumnAmount},@{ColumnTransactionDate},@{ColumnDescription},@{ColumnEmitterName},@{ColumnReceiverName})";
        public IEnumerable<ITransaction> Query(IUser user)
        {
            IList<ITransaction> res = new List<ITransaction>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = ReqQuery;
                command.Parameters.AddWithValue($"@{ColumnEmitterId}",user.Id);
                command.Parameters.AddWithValue($"@{ColumnReceiverId}",user.Id);
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
                command.CommandText = ReqCreate;
                command.Parameters.AddWithValue($"@{ColumnEmitterId}",transaction.EmitterId);
                command.Parameters.AddWithValue($"@{ColumnReceiverId}",transaction.ReceiverId);
                command.Parameters.AddWithValue($"@{ColumnAmount}",transaction.Amount);
                command.Parameters.AddWithValue($"@{ColumnTransactionDate}",transaction.TransactionDate);
                command.Parameters.AddWithValue($"@{ColumnDescription}",transaction.Description);
                command.Parameters.AddWithValue($"@{ColumnEmitterName}",transaction.EmitterName);
                command.Parameters.AddWithValue($"@{ColumnReceiverName}",transaction.ReceiverName);
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