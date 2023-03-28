using Business_Logic_Layer.Interface;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using JwtWebApiDotNet7.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/v1/[controller]")] 
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly InterfaceUser _IUser;
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration, InterfaceUser interfaceUser)
        {
            _configuration = configuration;
            _IUser = interfaceUser;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<TokenResponse> Login(UserLoginDTO request)
        {
            var userModel = _IUser.GetUserByUsername(request.Login);
            if (userModel == null)
            {
                return BadRequest("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, userModel.Password))
            {
                return BadRequest("Wrong password.");
            }
            String token = CreateToken(userModel);
            String refreshToken = GenerateRefreshToken();

            return Ok(new TokenResponse(token, refreshToken));
        }
        [HttpPost("refresh")]
        [Consumes("application/x-www-form-urlencoded")]
        [Authorize(
             AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
             Roles = "USER"
        )]
        public ActionResult<TokenResponse> RefreshToken([FromForm] string token, [FromForm] string refreshToken)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            var username = (principal.Identity as ClaimsIdentity)
                .Claims
                .Where(c => c.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault();
            var roles = (principal.Identity as ClaimsIdentity)
                            .Claims
                            .Where(c => c.Type == ClaimTypes.Role).FirstOrDefault();
            var email = (principal.Identity as ClaimsIdentity)
                .Claims
                .Where(c => c.Type == JwtRegisteredClaimNames.Email).FirstOrDefault();

            if (!IsRefreshInvalid(refreshToken))
            {
                UserModel userModel = new UserModel();
                userModel.Login = username.Value;
                userModel.TypeUserId = roles.Value;
                userModel.Email = email.Value;
                String newToken = CreateToken(userModel);
                String newRefreshToken = GenerateRefreshToken();

                return Ok(new TokenResponse(token, refreshToken));
            }
            return BadRequest();

        }

        private bool IsRefreshInvalid(string refreshToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                                     _configuration["JWT:Secret"])),
                ValidateLifetime = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(refreshToken, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return false;
        }
        private string CreateToken(UserModel user)
        {

            List<string> roles = new()
            {
                //CHANGE IMPLEMENTATION FOR ROLES FOR FUTURE NEW ROLES AND RETTRIEVE FROM DB
                "USER"
            };
            if (user.TypeUserId.Equals("0"))
            {
                roles.Add("ADMIN");
            }
            if (user.TypeUserId.Equals("1"))
            {
                roles.Add("RH");
            }
            //END
            List<Claim> claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, user.Login),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
               _configuration["JWT:Secret"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //Languages claim
            claims.Add(new Claim(ClaimTypes.Country, user.LanguageId));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.LoginId));
            //lg claim

            var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string GenerateRefreshToken()
        {

            List<Claim> claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
               _configuration["JWT:Secret"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
               _configuration["JWT:Secret"])),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

    }
}
