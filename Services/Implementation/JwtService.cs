using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Models;

namespace JobListing.Core.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(User u, List<string> roles)
        {
            var claims = new List<Claim>();
            var claim = new Claim(ClaimTypes.NameIdentifier, "MyClaim");
            claims.Add(claim);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.Now.AddDays(2)
            };
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);
            var t = handler.WriteToken(token);
            return t;
        }
    }
}
