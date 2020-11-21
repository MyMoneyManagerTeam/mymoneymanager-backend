using System;
using System.Collections.Generic;
using Domain.Users;
using Microsoft.AspNetCore.Http;

namespace Application.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<IUser> Query();
        IUser Get(string mail, string password);
        IUser Create(IUser user);
        bool Update(int id, IUser user);
        bool Delete(int id);
        bool UploadImage(Guid userId, IFormFile image);
    }
}