using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using BAL.Entities;
using BAL.ViewModels.User;
using EcoTechAPIs.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository;

namespace EcoTechAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        #region [ Local Variables ]
        IUnitOfWork uow;
        private readonly AppSettings _appSettings;
        public TokenController(IUnitOfWork _uow, IOptions<AppSettings> appSettings)
        {
            uow = _uow;
            _appSettings = appSettings.Value;
        }
        #endregion

        #region [ First Validate User & Generate Token ]
        /// <summary>
        /// First Validate User and then Generate Token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [Route("{create}/{username}/{password}")]
        public IActionResult Create(string UserName, string Password)
        {
            User _user = ValidateUser(UserName, Password);
            if (_user != null)
            {
                string _token = GenerateToken(UserName);
                //var response = new UserTokenResponse { user = _user, Token = _token };
                return Ok(_token);
            }
            else
                return BadRequest(new { message = "Username or password is incorrect" });
        }
        #endregion

        #region [ Validate User Details from DB ]
        /// <summary>
        /// Validate User Details from DB
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private User ValidateUser(string username, string password)
        {
            return uow.UserRepo.GetUsersWithRoles(username, password); ;
        }
        #endregion

        #region [ Generate Token ]
        /// <summary>
        /// Generate Token
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private string GenerateToken(string username)
        {
            // generate token that is valid for 1 day
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}