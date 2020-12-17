using System;
using System.Collections.Generic;
using Application.Services.Accounts.Dto;

namespace Application.Services.Accounts
{
    public interface IAccountService
    {
        OutputDtoGetAccount Get(Guid userId);
        OutputDtoCreateAccount Create(Guid userId);
        bool Update(Guid userId, InputDtoUpdateAccount account);
        bool ModifyBalance(Guid userId, double amount);
        
    }
}