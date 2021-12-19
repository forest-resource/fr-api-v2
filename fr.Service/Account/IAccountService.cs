using fr.Service.Interfaces;
using fr.Service.Model.Account;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace fr.Service.Account
{
    public interface IAccountService : IGeneratorService
    {
        Task<UserProfile> RegisterAsync(RegisterModel model);
        Task SeedAdminAccount(string adminUsername, string adminPassword);
        Task SeedAllRoles();
        Task<UserProfile> LoginAsync(LoginModel model);

        string CreateToken(UserProfile profile, IEnumerable<Claim> claims);
    }
}
