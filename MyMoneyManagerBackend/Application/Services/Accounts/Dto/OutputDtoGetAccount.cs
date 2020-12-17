using System;

namespace Application.Services.Accounts.Dto
{
    public class OutputDtoGetAccount
    {
        public Guid Id { get; set; }
        public double Balance { get; set; }
        public double AvailableBalance { get; set; }
        
    }
}