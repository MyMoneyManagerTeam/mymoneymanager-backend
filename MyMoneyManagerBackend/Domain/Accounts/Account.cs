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

        protected bool Equals(Account other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            return Equals((Account) obj);
        }
    }
}