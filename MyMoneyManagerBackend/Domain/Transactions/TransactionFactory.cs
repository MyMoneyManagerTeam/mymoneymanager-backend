using System;
using System.Data.SqlClient;
using Domain.Transactions;

namespace Domain.Transactions
{
    public class TransactionFactory: ITransactionFactory
    {
        public ITransaction CreateFromParam(Guid emitterId, Guid receiverId, double amount, DateTime transactionDate,
            string description, string emitterName, string receiverName)
        {
            return new Transaction
            {
                EmitterId = emitterId,
                ReceiverId = receiverId,
                Amount = amount,
                Description = description,
                EmitterName = emitterName,
                ReceiverName = receiverName,
                TransactionDate = transactionDate
            };
        }
    }
}