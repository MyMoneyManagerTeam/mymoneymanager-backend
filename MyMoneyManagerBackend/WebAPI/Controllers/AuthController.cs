using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Services.Accounts;
using Application.Services.Users;
using Application.Services.Users.Dto;
using Domain.Accounts;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMoneyManagerBackend.Utils;
using Newtonsoft.Json.Linq;

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
        private readonly IAccountService _accountService;

        public AuthController(IUserService userService, IAccountService accountService)
        {
            _userService = userService;
            _accountService = accountService;
        }
        [HttpPost]
        [Route("[action]")]
        //Action pour une requête de connexion. Si les infos sont correctes, on retourne les infos de l'utilisateur
        public ActionResult<OutputDtoAuth> Authenticate([FromBody] InputDtoAuth req)
        /*
         * Exemple d'un InputDtoAuth
            {
                "mail": "alexian.moins@outlook.com",
                "password": "azerty123"
            }
         */
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
        /*
         * Exemple d'InputDtoSignin
           {
                "mail": "alexian.moins@outlook.com",
                "firstName": "Alexian",
                "lastName": "Moins",
                "password": "azerty123",
                "country": "Belgique",
                "area": "Hainaut",
                "address": "Rue Victor Delporte 171",
                "zip": 7370,
                "city": "Dour",
                "picture": ""
            }
         */
        {
            //On demande au UserService de s'inscrire, si impossible il renverra null
            var response = _userService.Signin(user);
            if (response==null)
            {
                return BadRequest(new {message="Un compte existe déjà pour cette adresse mail"});
            }
            // Si ok on crée le compte bancaire
            _accountService.Create(response.Id);
            
            //On lie le token aux données de l'utilisateur
            response.Token = AuthUtils.GenerateToken(response);
            
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public ActionResult<bool> UploadImage(IFormFile picture)
        {
            if (_userService.UploadImage(new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value), picture))
                return Ok();
            else
                return BadRequest(new {message = "Impossible d'ajouter l'image"});
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