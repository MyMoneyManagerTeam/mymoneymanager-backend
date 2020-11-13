using Application.Services.Users.Dto;

namespace Application.Services.Users
{
    public interface IUserService
    {
        OutputDtoAuth Authenticate(InputDtoAuth inputDtoAuth);
        OutputDtoSignin Signin(InputDtoSignin inputDtoSignin);
    }
}