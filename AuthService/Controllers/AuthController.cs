using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BAL.ViewModels;
using BAL.ViewModels.User;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace AuthService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IConfiguration config;
        public AuthController(IConfiguration _config)
        {
            config = _config;
        }
        [HttpPost]
        public IActionResult ValidateUser([FromBody] UserViewModel model)
        {
            if (model.UserName == "user@gmail.com" && model.Password == "123456")
            {
                model.Roles = "User";

                var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
                var credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

                var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, model.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, model.UserName),
                    new Claim("Roles", model.Roles),
                    new Claim("CreatedDate", DateTime.Now.ToString())
                };

                var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Audience"], claims, 
                                                expires: DateTime.Now.AddMinutes(120),
                                                signingCredentials:credentials
                                                );

                string encToken = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(encToken);
            }
            else
            {
                return NoContent();
            }
        }
    }
}