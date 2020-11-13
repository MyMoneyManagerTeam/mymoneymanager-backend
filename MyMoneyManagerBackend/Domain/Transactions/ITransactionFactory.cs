using System;
using System.Data.SqlClient;
using Domain.Transactions;

namespace Domain.Transactions
{
    public interface ITransactionFactory
    {
        public ITransaction CreateFromParam(Guid emitterId, Guid receiverId, double amount, DateTime transactionDate, string description, string emitterName, string receiverName);
    }
}