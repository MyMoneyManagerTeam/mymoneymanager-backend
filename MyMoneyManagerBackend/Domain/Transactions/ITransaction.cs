using System;
using Domain.Shared;
using Domain.Users;

namespace Domain.Transactions
{
    public interface ITransaction: IEntity
    {
        User Emitter { get; set; }
        User Receiver { get; set; }
        double Amount { get; set; }
        DateTime TransactionDate { get; set; }
        string Description { get; set; }
        string EmitterNameCustom { get; set; }
        string ReceiverNameCustom { get; set; }
    }
}