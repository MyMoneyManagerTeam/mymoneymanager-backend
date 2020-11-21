using System;
using Application.Services.Users.Dto;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Users
{
    public interface IUserService
    {
        OutputDtoAuth Authenticate(InputDtoAuth inputDtoAuth);
        OutputDtoSignin Signin(InputDtoSignin inputDtoSignin);
        bool UploadImage(Guid userId,IFormFile image);
    }
}