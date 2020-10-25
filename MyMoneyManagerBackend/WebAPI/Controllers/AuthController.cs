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
    }

    public class AuthenticateRequest
    {
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}