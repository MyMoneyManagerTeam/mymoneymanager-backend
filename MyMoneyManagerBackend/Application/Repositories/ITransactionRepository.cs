using System;
using System.Collections.Generic;
using Domain.Transactions;
using Domain.Users;

namespace Application.Repositories
{
    public interface ITransactionRepository
    {
        IEnumerable<ITransaction> Query(Guid userId,int number, int page, int days);
        ITransaction Get(Guid id);
        ITransaction Create(ITransaction transaction);
        bool Update(Guid id, ITransaction transaction);
        bool Delete(Guid id);
    }
}