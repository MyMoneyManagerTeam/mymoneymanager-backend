using System;

namespace Application.Services.Accounts.Dto
{
    public class InputDtoModifyBalanceAccount
    {
        public Guid UserId { get; set; }
        public double Amount { get; set; }
    }
}