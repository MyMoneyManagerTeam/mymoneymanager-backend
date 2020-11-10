﻿using System;
using System.Collections.Generic;

namespace Application.Repositories
{
    public interface ITransactionRepository
    {
        IEnumerable<ITransaction> Query(IUser user);
        ITransaction Get(Guid id);
        ITransaction Create(ITransaction transaction);
        bool Update(Guid id, ITransaction transaction);
        bool Delete(Guid id);
    }
}