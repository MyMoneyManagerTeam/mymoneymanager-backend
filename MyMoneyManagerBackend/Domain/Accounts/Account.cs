using System;

namespace Domain.Accounts
{
    public class Account: IAccount
    {
        public Account()
        {
        }

        public Guid Id { get; set; }
        public double Balance { get; set; }
    }
}