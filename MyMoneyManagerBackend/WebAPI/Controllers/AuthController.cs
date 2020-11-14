using System.Security.Claims;
using Application.Services.Users;
using Application.Services.Users.Dto;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMoneyManagerBackend.Utils;

/*
 * ATTENTION RESTE A AJOUTER UNE FONCTION DE HASH POUR LE MDP.
 */
namespace MyMoneyManagerBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("[action]")]
        //Action pour une requête de connexion. Si les infos sont correctes, on retourne les infos de l'utilisateur
        public ActionResult<OutputDtoAuth> Authenticate([FromBody] InputDtoAuth req)
        {
            // On demande au UserService si les infos de connexion sont correctes. Retourne null si non.
            var response = _userService.Authenticate(req);
            if (response == null)
            {
                return BadRequest(new
                {
                    message = "Adresse mail ou mot de passe incorrect"
                });
            }
            
            //On lie le token aux données de l'utilisateur
            response.Token = AuthUtils.GenerateToken(response);

            return Ok(response);
        }
        [HttpPost]
        [Route("[action]")]
        //Action pour une requête d'inscription, Si l'ajout est possible, on retourne les infos de l'utilisateur
        public ActionResult<IUser> Signin([FromBody] InputDtoSignin user)
        {
            //On demande au UserService de s'inscrire, si impossible il renverra null
            var response = _userService.Signin(user);
            if (response==null)
            {
                return BadRequest(new {message="Un compte existe déjà pour cette adresse mail"});
            }
            
            //On lie le token aux données de l'utilisateur
            response.Token = AuthUtils.GenerateToken(response);
            
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("tokentestadmin")]
        public ActionResult Admin()
        {
            return Ok(new {message = "Vous êtes bien un administrateur," +
                                     $" vous êtes {User.FindFirst(ClaimTypes.Name)?.Value} et votre id" +
                                     $" est {User.FindFirst(ClaimTypes.NameIdentifier)?.Value}"});
        }
        [HttpGet]
        [Authorize]
        [Route("tokentestlambda")]
        
        public ActionResult TokenTest()
        {
            return Ok(new {message = "Votre JWT a bien été validé," +
                                     $" vous êtes {User.FindFirst(ClaimTypes.Name)?.Value} et votre id" +
                                     $" est {User.FindFirst(ClaimTypes.NameIdentifier)?.Value}"});
        }
    }
}