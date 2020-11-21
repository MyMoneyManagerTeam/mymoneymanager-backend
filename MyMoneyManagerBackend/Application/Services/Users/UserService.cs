using System;
using Application.Repositories;
using Application.Services.Accounts;
using Application.Services.Users.Dto;
using Domain.Users;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Users
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFactory _userFactory = new UserFactory();

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public OutputDtoAuth Authenticate(InputDtoAuth inputDtoAuth)
        {
            var userFromDto = _userFactory.GetFromParam(inputDtoAuth.Mail.ToLower(),inputDtoAuth.Password);
            var userInDb = _userRepository.Get(userFromDto.Mail,userFromDto.Password);
            if (userInDb == null)
            {
                return null;
            }
            return new OutputDtoAuth
            {
                Id = userInDb.Id,
                Address = userInDb.Address,
                Admin = userInDb.Admin,
                Area = userInDb.Area,
                City = userInDb.City,
                Confirmed = userInDb.Confirmed,
                Country = userInDb.Country,
                Mail = userInDb.Mail,
                Picture = userInDb.Picture,
                Zip = userInDb.Zip,
                FirstName = userInDb.FirstName,
                LastName = userInDb.LastName,
            };
        }

        public OutputDtoSignin Signin(InputDtoSignin inputDtoSignin)
        {
            var i = inputDtoSignin;
            var userFromDto = _userFactory.CreateFromParam(i.Mail,i.Password,i.FirstName,i.LastName,null,i.Country,i.Area,i.Address,i.Zip,i.City);
            var userInDb = _userRepository.Create(userFromDto);
            if (userInDb == null)
                return null;
            return new OutputDtoSignin
            {
                Id = userInDb.Id,
                Address = userInDb.Address,
                Admin = userInDb.Admin,
                Area = userInDb.Area,
                City = userInDb.City,
                Confirmed = userInDb.Confirmed,
                Country = userInDb.Country,
                Mail = userInDb.Mail,
                Picture = userInDb.Picture,
                Zip = userInDb.Zip,
                FirstName = userInDb.FirstName,
                LastName = userInDb.LastName,
            };
        }

        public bool UploadImage(Guid userId, IFormFile image)
        {
            return _userRepository.UploadImage(userId, image);
        }
    }
}