using System;

namespace Application.Services.Accounts.Dto
{
    public class InputDtoUpdateAccount
    {
        public Guid Id { get; set; }
        public double Balance { get; set; }
    }
}