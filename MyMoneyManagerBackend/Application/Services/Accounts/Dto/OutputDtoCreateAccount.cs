using System;

namespace Application.Services.Accounts.Dto
{
    public class OutputDtoCreateAccount
    {
        public Guid Id { get; set; }
        public double Balance { get; set; }

        protected bool Equals(OutputDtoCreateAccount other)
        {
            return Id.Equals(other.Id) && Balance.Equals(other.Balance);
        }

        public override bool Equals(object obj)
        {
            return Equals((OutputDtoCreateAccount) obj);
        }
    }
}