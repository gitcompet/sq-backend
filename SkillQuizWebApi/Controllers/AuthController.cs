using Business_Logic_Layer.Interface;
using Business_Logic_Layer.Models;
using JwtWebApiDotNet7.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Extensions;
using SkillQuizWebApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JwtWebApiDotNet7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly InterfaceUser _IUser;
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration,InterfaceUser interfaceUser)
        {
            _configuration = configuration;
            _IUser = interfaceUser;
        }
          

        [HttpPost("login")]
        public ActionResult<TokenResponse> Login(UserLoginDTO request)
        {
            var userModel = _IUser.GetUserByUsername(request.Login);
            if (userModel ==  null)
            {
                return BadRequest("User not found.");
            }
              
           if (!BCrypt.Net.BCrypt.Verify(request.Password, userModel.Password))
            {
                return BadRequest("Wrong password.");
            }   
            string token = CreateToken(userModel);
            string refreshToken = GenerateRefreshToken();

            return Ok(new TokenResponse(token,refreshToken));
        }

        private string CreateToken(UserModel user)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            List<string> roles = new()
            {
                //CHANGE IMPLEMENTATION FOR ROLES FOR FUTURE NEW ROLES AND RETTRIEVE FROM DB
                "USER"
            };
            if (user.TypeUserId.Equals("0")) {
                roles.Add("ADMIN");                
            }
            if (user.TypeUserId.Equals("1"))
            {
                roles.Add("RH");
            }
            //END
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Login)             
            };
            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    issuer: "http://localhost:63869",
                    audience: "http://localhost:63869",
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
