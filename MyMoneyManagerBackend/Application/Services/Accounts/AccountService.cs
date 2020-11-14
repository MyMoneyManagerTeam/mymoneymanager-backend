using System;
using Application.Repositories;
using Application.Services.Accounts.Dto;
using Application.Services.Users;
using Domain.Accounts;

namespace Application.Services.Accounts
{
    
    public class AccountService: IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountFactory _accountFactory = new AccountFactory();
        
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public OutputDtoGetAccount Get(Guid userId)
        {
            var accountInDb = _accountRepository.Get(userId);
            if (accountInDb == null)
            {
                return null;
            }
            return new OutputDtoGetAccount()
            {
                Balance = accountInDb.Balance,
                Id = accountInDb.Id
            };
        }

        public OutputDtoCreateAccount Create(Guid userId)
        {
            var accountInDb = _accountRepository.Create(userId);
            if (accountInDb == null)
            {
                return null;
            }
            return new OutputDtoCreateAccount()
            {
                Balance = accountInDb.Balance,
                Id = accountInDb.Id
            };
        }

        public bool Update(Guid userId, InputDtoUpdateAccount account)
        {
            var accountFromDto = _accountFactory.GetFromParam(account.Id, account.Balance);
            return _accountRepository.Update(userId, accountFromDto);
        }
    }
}