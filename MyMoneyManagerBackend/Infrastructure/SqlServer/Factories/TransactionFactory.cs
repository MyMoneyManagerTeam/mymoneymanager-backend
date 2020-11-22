using System;
using System.Data.SqlClient;
using Domain.Transactions;
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
                EmitterId = reader.GetGuid(reader.GetOrdinal(TransactionSqlServer.ColumnEmitterId)),
                ReceiverId = reader.GetGuid(reader.GetOrdinal(TransactionSqlServer.ColumnReceiverId)),
                Amount = reader.GetDouble(reader.GetOrdinal(TransactionSqlServer.ColumnAmount)),
                TransactionDate = reader.GetDateTime(reader.GetOrdinal(TransactionSqlServer.ColumnTransactionDate)),
                Description = reader.GetString(reader.GetOrdinal(TransactionSqlServer.ColumnDescription)),
                EmitterName = reader.GetString(reader.GetOrdinal(TransactionSqlServer.ColumnEmitterName)),
                ReceiverName = reader.GetString(reader.GetOrdinal(TransactionSqlServer.ColumnReceiverName))
            };
        }
    }
}