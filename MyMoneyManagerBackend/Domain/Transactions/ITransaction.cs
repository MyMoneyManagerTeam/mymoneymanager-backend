using System;
using Domain.Shared;

namespace Domain.Transactions
{
    public interface ITransaction: IEntity
    {
        Guid EmitterId { get; set; }
        Guid ReceiverId { get; set; }
        double Amount { get; set; }
        DateTime TransactionDate { get; set; }
        string Description { get; set; }
        string EmitterName { get; set; }
        string ReceiverName { get; set; }
    }
}