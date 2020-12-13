using System;
using System.Data.SqlClient;
using Domain.Transactions;
using Domain.Users;

namespace Domain.Transactions
{
    public interface ITransactionFactory
    {
        public ITransaction CreateFromParam(User emitter, User receiver, double amount, DateTime transactionDate, string description, string emitterName, string receiverName);
    }
}