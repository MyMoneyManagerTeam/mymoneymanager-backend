using System;
using Application.Services.Accounts.Dto;

namespace Application.Services.Accounts
{
    public interface IAccountService
    {
        OutputDtoGetAccount Get(Guid userId);
        OutputDtoCreateAccount Create(Guid userId);
        bool Update(Guid userId, InputDtoUpdateAccount account);
    }
}