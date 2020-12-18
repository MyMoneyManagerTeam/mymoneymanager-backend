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
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Account) obj);
        }
    }
}