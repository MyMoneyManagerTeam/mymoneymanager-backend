using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using Application.Repositories;
using Application.Services.Users;
using Application.Services.Users.Dto;
using Domain.Users;
using Infrastructure.SqlServer.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MyMoneyManagerBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        #region Token Region
        public string GenerateToken(IOutputDtoAuthSign user)
        {
            var token = new JwtSecurityToken(
                claims:    new Claim[] { 
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Mail),
                    new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                    new Claim(ClaimTypes.Role, user.Admin?"admin":"lambda"),
                },
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires:   new DateTimeOffset(DateTime.Now.AddMinutes(10080)).DateTime,
                signingCredentials: new SigningCredentials(SIGNING_KEY,
                    SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private const string SECRET_KEY = "eUepR6WS9t7J9IgZUa5OPNQbxYzfn9mk6YoEkkp5Es4=";
        public static readonly SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
        #endregion
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("[action]")]
        public ActionResult<IUser> Authenticate([FromBody] InputDtoAuth req)
        {
            var response = _userService.Authenticate(req);
            if (response == null)
            {
                return BadRequest(new
                {
                    message = "Adresse mail ou mot de passe incorrect"
                });
            }

            response.Token = GenerateToken(response);

            return Ok(response);
        }
        [HttpPost]
        [Route("[action]")]
        public ActionResult<IUser> Signin([FromBody] InputDtoSignin user)
        {
            var response = _userService.Signin(user);
            if (response==null)
            {
                return BadRequest(new {message="Un compte existe déjà pour cette adresse mail"});
            }

            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("tokentestadmin")]
        public ActionResult Admin()
        {
            return Ok(new {message = "OK TOUT SE PASSE BIEN AVEC LE TOKEN ADMIN"});
        }
        [HttpGet]
        [Authorize]
        [Route("tokentestlambda")]
        
        public ActionResult TokenTest()
        {
            return Ok(new {message = "OK TOUT SE PASSE BIEN AVEC LE TOKEN LAMBDA"});
        }
    }
}