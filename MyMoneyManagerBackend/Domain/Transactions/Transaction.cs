﻿using System;
using Domain.Users;

namespace Domain.Transactions
{
    public class Transaction: ITransaction
    {
        public Guid Id { get; set; }
        public IUser Emitter { get; set; } //remplacer par des user plutot que les id
        public IUser Receiver { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public string EmitterNameCustom { get; set; } // peut etre changer le nom par custom
        public string ReceiverNameCustom { get; set; }

        public Transaction()
        {
        }

        protected bool Equals(Transaction other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            return Equals((Transaction) obj);
        }
    }
}