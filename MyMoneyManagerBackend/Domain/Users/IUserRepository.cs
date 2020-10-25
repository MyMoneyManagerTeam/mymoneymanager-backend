using System.Collections;
using System.Collections.Generic;

namespace Domain.Users
{
    public interface IUserRepository
    {
        IEnumerable<IUser> Query();
        IUser Get(string mail, string password);
        IUser Create(IUser user);
        bool Update(int id, IUser user);
        bool Delete(int id);
    }
}