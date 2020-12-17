using System;
using System.Data.SqlClient;
using Domain.Transactions;
using Domain.Users;
using Infrastructure.SqlServer.Transactions;

namespace Infrastructure.SqlServer.Factories
{
    public class TransactionFactory: IInstanceFromReaderFactory<ITransaction>
    {
        public ITransaction CreateFromReader(SqlDataReader reader)
        {
            return new Transaction()
            {
                Id = reader.GetGuid(reader.GetOrdinal(TransactionSqlServer.ColumnId)),
                Emitter = new User{Id = reader.GetGuid(reader.GetOrdinal(TransactionSqlServer.ColumnEmitterId))},
                Receiver = new User{Id =reader.GetGuid(reader.GetOrdinal(TransactionSqlServer.ColumnReceiverId))},
                Amount = reader.GetDouble(reader.GetOrdinal(TransactionSqlServer.ColumnAmount)),
                TransactionDate = reader.GetDateTime(reader.GetOrdinal(TransactionSqlServer.ColumnTransactionDate)),
                Description = reader.GetString(reader.GetOrdinal(TransactionSqlServer.ColumnDescription)),
                EmitterNameCustom = reader.GetString(reader.GetOrdinal(TransactionSqlServer.ColumnEmitterName)),
                ReceiverNameCustom = reader.GetString(reader.GetOrdinal(TransactionSqlServer.ColumnReceiverName))
            };
        }

        public ITransaction CreateFromReaderWithAccount(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}