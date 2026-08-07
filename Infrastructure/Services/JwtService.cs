using Application.Common;
using Application.Interface.Service;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class JwtService : IJwtService
    {

        private readonly JwtConfig _jwt;

        public JwtService(IOptions<JwtConfig> jwt)
        {

            _jwt = jwt.Value;
        }

        public string GenerateToken(string userId, string userName, string Role)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, Role),

                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
                var Creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(_jwt.ExpiresMinutes),
                    signingCredentials: Creds,
                    issuer: _jwt.Issuer,
                    audience: _jwt.Audience
                    );

                return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            }
            catch (Exception ex)

            {

                throw new Exception("Error generating token" + ex.Message);
            }

        }


    }
}
