using System;

namespace Application.Services.Accounts.Dto
{
    public class OutputDtoGetAccount
    {
        public Guid Id { get; set; }
        public double Balance { get; set; }
        public double AvailableBalance { get; set; }

        protected bool Equals(OutputDtoGetAccount other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            return Equals((OutputDtoGetAccount) obj);
        }
    }
}