using System;
using System.Collections.Generic;
using Domain.Jars;
using Domain.Users;

namespace Application.Repositories
{
    public interface IJarRepository
    {
        IEnumerable<IJar> Query(Guid userId);
        IJar Get(Guid userId, Guid jarId);
        IJar Create(IJar jar);
        bool Update(Guid id, IJar jar);
        bool Delete(Guid userId, Guid jarId);
        double TotalBalanceByUserId(Guid userId);
    }
}