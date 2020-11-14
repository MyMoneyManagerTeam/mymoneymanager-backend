using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Services.Users.Dto;
using Microsoft.IdentityModel.Tokens;

namespace MyMoneyManagerBackend.Utils
{
    public class AuthUtils
    {
        #region Token Region
        
        
        //Ici la clé secrète pour le cryptage du token
        private const string SECRET_KEY = "eUepR6WS9t7J9IgZUa5OPNQbxYzfn9mk6YoEkkp5Es4=";
        
        
        
        //Ici la signing key étant une clé de sécurité symmetrique (https://fr.wikipedia.org/wiki/Cryptographie_symétrique)
        public static readonly SymmetricSecurityKey SIGNING_KEY = 
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
        
        
        
        
        //On génère un token en fonction des informations propres à l'utilisateur.
        //En paramètre, l'objet qu'on va lui renvoyer, représentant un utilisateur.
        public static string GenerateToken(IOutputDtoAuthSign user)
        {
            var token = new JwtSecurityToken(
                //on défini les claims du token
                claims:    new Claim[] { 
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Mail),
                    new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                    new Claim(ClaimTypes.Role, user.Admin?"admin":"lambda"),
                },
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                //un token expire au bout de 7 jours
                expires:   new DateTimeOffset(DateTime.Now.AddMinutes(10080)).DateTime,
                //on décide quel algo de cryptage on utilise, et on lui donne la clé.
                signingCredentials: new SigningCredentials(SIGNING_KEY,
                    SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        #endregion
    }
}