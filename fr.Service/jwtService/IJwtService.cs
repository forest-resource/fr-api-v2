using fr.Service.Interfaces;
using fr.Service.Model.Account;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Security.Claims;

namespace fr.Service.jwtService
{
    public interface IJwtService : IGeneratorService
    {
        string CreateToken(UserProfile userProfile, IEnumerable<Claim> claims);
        TokenValidationResult ValidateToken(string token);
    }
}
