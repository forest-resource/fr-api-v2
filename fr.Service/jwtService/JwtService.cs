using fr.Service.Model.Account;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace fr.Service.jwtService
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration configuration;

        public JwtService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateToken(UserProfile userProfile, IEnumerable<Claim> claims)
        {
            var key = configuration["Authentication:JwtSettings:Key"];
            var issuer = configuration["Authentication:JwtSettings:Issuer"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:JwtSettings:Key"]));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var payload = JsonConvert.SerializeObject(
                claims
                .Union(new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userProfile.Email),
                    new Claim(ClaimTypes.Name, userProfile.Email),
                    new Claim(ClaimTypes.NameIdentifier, userProfile.FullName),
                    new Claim(ClaimTypes.GivenName, userProfile.FullName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iss, issuer),
                    new Claim(JwtRegisteredClaimNames.Aud, issuer)
                })
                .ToDictionary(r => r.Type, r => r.Value));

            var tokenHandler = new JsonWebTokenHandler
            {
                TokenLifetimeInMinutes = 120
            };
            return tokenHandler.CreateToken(payload, credential);
        }

        public TokenValidationResult ValidateToken(string token)
        {
            var key = configuration["Authentication:JwtSettings:Key"];
            var issuer = configuration["Authentication:JwtSettings:Issuer"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = credential.Key,
                RequireExpirationTime = true,
                ValidIssuer = issuer,
                ValidAudience = issuer,
                ValidAudiences = new List<string> { issuer },
                ValidateAudience = false,
                ValidateIssuer = false
            };

            var tokenHandler = new JsonWebTokenHandler
            {
                TokenLifetimeInMinutes = 120
            };

            return tokenHandler.ValidateToken(token, tokenValidationParameters);
        }
    }
}
