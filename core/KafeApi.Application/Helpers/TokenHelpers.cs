using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Helpers
{
    public class TokenHelpers
    {
        private readonly IConfiguration _configuration;

        public TokenHelpers(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public  string GenerateToken(string email)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creadentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // kullanıcı bilgileri al
            var claim = new List<Claim>
            {
                new Claim("email",email),
                new Claim("role","Admin"),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claim,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creadentials
             );

            var resultToken = new JwtSecurityTokenHandler().WriteToken(token);
            return resultToken;
        }
    }
}
