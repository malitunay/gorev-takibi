using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Project.Dal.Abstract;
using Project.Entity.Dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bll
{
    public class TokenManager
    {
        IConfiguration _configuration;
        public TokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateAccessToken(DtoLoginUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.RoleName)
            };
            
            var claimsIdentity = new ClaimsIdentity(claims,"Token");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Tokens:Issuer"],
                audience: _configuration["Tokens:Audience"],
                expires: DateTime.Now.AddHours(24),
                notBefore: DateTime.Now,
                signingCredentials: cred,
                claims : claimsIdentity.Claims
                );

            var tokenHandler = new {token = new JwtSecurityTokenHandler().WriteToken(token)};

            return tokenHandler.token;
        }
    }
}
