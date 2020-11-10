using System;
using System.Collections.Generic;

namespace Application.Repositories
{
    public interface IAccountRepository
    {
        IEnumerable<IAccount> Query();
        IAccount Get(Guid id);
        IAccount Create(IUser user);
        bool Update(Guid id, IAccount account);
        bool Delete(Guid id);
    }
}