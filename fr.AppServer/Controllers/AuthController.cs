using fr.Service.Account;
using fr.Service.Model.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace fr.AppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AuthController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public Task<UserProfile> Register([FromBody] RegisterModel model)
            => accountService.RegisterAsync(model);

        [HttpPost("login")]
        [AllowAnonymous]
        public Task<UserProfile> Login([FromBody] LoginModel model)
            => accountService.LoginAsync(model);
    }
}
