using System;
using System.Net.Http;
using Domain.Users;
using Infrastructure.SqlServer.Auth;
using Microsoft.AspNetCore.Mvc;

namespace MyMoneyManagerBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private IUserRepository _userRepository = new SqlServerUserRepository();
        [HttpPost]
        [Route("[action]")]
        public ActionResult<IUser> Authenticate([FromBody] AuthenticateRequest req)
        {
            Console.Write("Auth");
            var response = _userRepository.Get(req.Mail,req.Password);
            if (response == null)
            {
                return BadRequest(new
                {
                    message = "Adresse mail ou mot de passe incorrect"
                });
            }

            return Ok(response);
        }
        [HttpPost]
        [Route("[action]")]
        public ActionResult<IUser> Signin([FromBody] SigninRequest req)
        {
            var response = _userRepository.Create(new User(int.MinValue,req.Mail,req.Password,req.FirstName,req.LastName,req.Country,req.Area,req.Address,int.Parse(req.ZipCode),req.City,null,null,null));
            if (response.Id == int.MinValue)
            {
                return BadRequest(
                    new {message = "Utilisateur déjà inscrit"});
            }
            
            return Ok(response);
        }
    }

    public class AuthenticateRequest
    {
        public string Mail { get; set; }
        public string Password { get; set; }
    }

    public class SigninRequest
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}