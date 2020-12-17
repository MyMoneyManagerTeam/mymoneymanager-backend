using System;
using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.Accounts.Dto;
using Application.Services.Jars;
using Application.Services.Jars.Dto;
using Application.Services.Users;
using Domain.Accounts;

namespace Application.Services.Accounts
{
    
    public class AccountService: IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountFactory _accountFactory = new AccountFactory();
        private readonly IJarRepository _jarRepository;
        
        public AccountService(IAccountRepository accountRepository, IJarRepository jarRepository)
        {
            _accountRepository = accountRepository;
            _jarRepository = jarRepository;
        }

        public OutputDtoGetAccount Get(Guid userId)
        {
            var accountInDb = _accountRepository.Get(userId);
            var availableBalance = _jarRepository.TotalBalanceByUserId(userId);
            if (accountInDb == null)
            {
                return null;
            }
            return new OutputDtoGetAccount()
            {
                Balance = accountInDb.Balance,
                Id = accountInDb.Id,
                AvailableBalance = accountInDb.Balance-availableBalance
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

        public bool ModifyBalance(Guid userId, double amount)
        {
            return _accountRepository.ModifyBalance(userId, amount);
        }
        
    }
}