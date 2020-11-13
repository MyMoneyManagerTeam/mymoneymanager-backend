using System;
using System.Collections.Generic;
using Domain.Jars;
using Domain.Users;

namespace Application.Repositories
{
    public interface IJarRepository
    {
        IEnumerable<IJar> Query(IUser user);
        IJar Get(Guid id);
        IJar Create(IJar jar);
        bool Update(Guid id, IJar jar);
        bool Delete(Guid id);
    }
}