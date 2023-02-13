using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProblemTracking.Entity.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ProblemTracking.Web.Services;
using ProblemTracking.Web.Model;
using Microsoft.Extensions.Configuration;

namespace ProblemTracking.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class LoginController : BaseController<UserService>
    {
        private readonly IConfiguration _config;
        public LoginController(IConfiguration config, IServiceFactory serviceFactory) : base(serviceFactory)
        {
            _config = config;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserViewModel login)
        {
            IActionResult response = Unauthorized();

            UserViewModel user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJWT(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }

            return response;
        }

        UserViewModel AuthenticateUser(UserViewModel loginCredentials)
        {
            UserViewModel user = Service.AuthenUser(loginCredentials);
            return user;
        }

        string GenerateJWT(UserViewModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim("firstName", userInfo.FirstName.ToString()),
                new Claim("role",userInfo.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
