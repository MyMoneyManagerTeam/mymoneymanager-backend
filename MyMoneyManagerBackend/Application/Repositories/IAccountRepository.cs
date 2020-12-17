using System;
using System.Collections.Generic;
using Domain.Accounts;
using Domain.Users;

namespace Application.Repositories
{
    public interface IAccountRepository
    {
        IAccount Get(Guid id);
        IAccount Create(Guid userId);
        bool Update(Guid id, IAccount account);
        bool Delete(Guid id);
        bool ModifyBalance(Guid id, double amount);
        
    }
}