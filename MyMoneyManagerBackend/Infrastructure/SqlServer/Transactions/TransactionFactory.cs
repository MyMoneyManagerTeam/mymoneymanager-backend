using System.Data.SqlClient;
using Domain.Transactions;

namespace Infrastructure.SqlServer.Transactions
{
    public class TransactionFactory: ITransactionFactory
    {
        public ITransaction CreateFromReader(SqlDataReader reader)
        {
            return new Transaction()
            {
                Id = reader.GetGuid(reader.GetOrdinal(SqlServerTransactionRepository.ColumnId)),
                EmitterId = reader.GetGuid(reader.GetOrdinal(SqlServerTransactionRepository.ColumnEmitterId)),
                ReceiverId = reader.GetGuid(reader.GetOrdinal(SqlServerTransactionRepository.ColumnReceiverId)),
                Amount = reader.GetDouble(reader.GetOrdinal(SqlServerTransactionRepository.ColumnAmount)),
                TransactionDate = reader.GetDateTime(reader.GetOrdinal(SqlServerTransactionRepository.ColumnTransactionDate)),
                Description = reader.GetString(reader.GetOrdinal(SqlServerTransactionRepository.ColumnDescription)),
                EmitterName = reader.GetString(reader.GetOrdinal(SqlServerTransactionRepository.ColumnEmitterName)),
                ReceiverName = reader.GetString(reader.GetOrdinal(SqlServerTransactionRepository.ColumnReceiverName))
            };
        }
    }
}