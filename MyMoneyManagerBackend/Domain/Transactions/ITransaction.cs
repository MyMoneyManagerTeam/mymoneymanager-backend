using System;
using Domain.Shared;
using Domain.Users;

namespace Domain.Transactions
{
    public interface ITransaction: IEntity
    {
        IUser Emitter { get; set; }
        IUser Receiver { get; set; }
        double Amount { get; set; }
        DateTime TransactionDate { get; set; }
        string Description { get; set; }
        string EmitterNameCustom { get; set; }
        string ReceiverNameCustom { get; set; }
    }
}