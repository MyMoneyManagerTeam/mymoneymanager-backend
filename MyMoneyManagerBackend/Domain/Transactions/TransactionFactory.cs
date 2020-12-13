using System;
using System.Data.SqlClient;
using Domain.Transactions;
using Domain.Users;

namespace Domain.Transactions
{
    public class TransactionFactory: ITransactionFactory
    {
        public ITransaction CreateFromParam(User emitter, User receiver, double amount, DateTime transactionDate,
            string description, string emitterName, string receiverName)
        {
            return new Transaction
            {
                Emitter = emitter,
                Receiver = receiver,
                Amount = amount,
                Description = description,
                EmitterNameCustom = emitterName,
                ReceiverNameCustom = receiverName,
                TransactionDate = transactionDate
            };
        }
    }
}