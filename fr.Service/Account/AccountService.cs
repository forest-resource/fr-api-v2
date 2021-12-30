using AutoMapper;
using fr.Core;
using fr.Core.Enumeration;
using fr.Core.Exceptions;
using fr.Core.Timing;
using fr.Database.Model.Entities.Users;
using fr.Service.jwtService;
using fr.Service.Model.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace fr.Service.Account
{
    public class AccountService : IAccountService
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Roles> roleManager;
        private readonly IConfiguration configuration;
        private readonly IJwtService jwtService;
        public AccountService(
            IMapper mapper,
            IConfiguration configuration,
            UserManager<User> userManager,
            RoleManager<Roles> roleManager,
            IJwtService jwtService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.configuration = configuration;
            this.roleManager = roleManager;
            this.jwtService = jwtService;
        }

        public async Task<UserProfile> LoginAsync(LoginModel model)
        {
            // Check if User is already in the System
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                throw new UnauthorizeException();

            // Check if User is locked or not
            if (await userManager.IsLockedOutAsync(user))
            {
                if (await userManager.IsInRoleAsync(user, ERoles.Administrator.ToString()))
                {
                    await userManager.ResetAccessFailedCountAsync(user);
                }
                throw new ForbiddenException();
            }

            // Check if User entered the right password
            if (!await userManager.CheckPasswordAsync(user, model.Password))
            {
                await userManager.AccessFailedAsync(user);
                throw new UnauthorizeException();
            }

            // Map user to UserProfile before return
            var userProfile = mapper.Map<User, UserProfile>(user);
            userProfile.Roles = (await userManager.GetRolesAsync(user)).ToList();

            // Get User Claims
            var claims = (await userManager.GetClaimsAsync(user)).AsEnumerable();
            // Get Roles Claims
            foreach (var roleName in userProfile.Roles)
            {
                // Get role of Users from roleName
                var role = await roleManager.FindByNameAsync(roleName);
                // Get Claims of the role
                var roleClaims = await roleManager.GetClaimsAsync(role);
                // Union with the userClaims
                claims = claims.Union(roleClaims.Where(r => r.Value == EAllowType.Allow.ToString()));
            }
            // Select only claim which is allow for user
            claims = claims.Distinct().Where(r => r.Value == EAllowType.Allow.ToString());
            userProfile.Claims = claims.Select(r => r.Type).ToList();
            userProfile.Schema = Constants.DefaultSchema;
            userProfile.Token = jwtService.CreateToken(userProfile, claims);

            return userProfile;
        }

        public async Task<UserProfile> RegisterAsync(RegisterModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                throw new AppException("User Exist");
            }

            var userCreatedResult = await userManager.CreateAsync(mapper.Map<User>(model), model.Password);
            if (userCreatedResult != null || !userCreatedResult.Succeeded)
            {
                throw new AppException($"Cannot create User {model.Email}");
            }

            user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return null;
            }

            var addToRoleResult = await userManager.AddToRoleAsync(user, ERoles.Student.ToString());

            if (addToRoleResult == null || !addToRoleResult.Succeeded)
            {
                return null;
            }

            // Map user to UserProfile before return
            var userProfile = mapper.Map<User, UserProfile>(user);
            userProfile.Roles = (await userManager.GetRolesAsync(user)).ToList();

            // Get User Claims
            var claims = (await userManager.GetClaimsAsync(user)).AsEnumerable();
            // Get Roles Claims
            foreach (var roleName in userProfile.Roles)
            {
                // Get role of Users from roleName
                var role = await roleManager.FindByNameAsync(roleName);
                // Get Claims of the role
                var roleClaims = await roleManager.GetClaimsAsync(role);
                // Union with the userClaims
                claims = claims.Union(roleClaims.Where(r => r.Value == EAllowType.Allow.ToString()));
            }
            // Select only claim which is allow for user
            claims = claims.Distinct().Where(r => r.Value == EAllowType.Allow.ToString());
            userProfile.Claims = claims.Select(r => r.Type).ToList();
            userProfile.Schema = Constants.DefaultSchema;
            userProfile.Token = jwtService.CreateToken(userProfile, claims);

            return userProfile;
        }

        public async Task SeedAdminAccount(string adminUsername, string adminPassword)
        {
            var user = await userManager.FindByNameAsync(adminUsername);
            if (user != null)
            {
                return;
            }

            user = new User
            {
                UserName = adminUsername,
                DayOfBird = Clock.Now,
                Email = $"{adminUsername}@admin.com",
                FirstName = adminUsername,
                LastName = adminUsername,
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = string.Empty,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                StudentCode = string.Empty
            };

            var userCreatedResult = await userManager.CreateAsync(user, adminPassword);

            if (!userCreatedResult.Succeeded)
            {
                throw new AppException("Cannot Create Admin Account");
            }

            await userManager.AddToRoleAsync(user, ERoles.Administrator.ToString());
        }

        public async Task SeedAllRoles()
        {
            var roleNames = Enum.GetNames(typeof(ERoles));

            foreach (string roleName in roleNames)
            {
                var role = new Roles
                {
                    Name = roleName
                };
                var roleCreatedResult = await roleManager.CreateAsync(role);

                if (!roleCreatedResult.Succeeded)
                {
                    continue;
                }

                var permissions = configuration.GetRequiredSection($"Administrator:Roles:{roleName}").Get<List<string>>();
                permissions ??= new List<string>();
                foreach (var permission in permissions)
                {
                    await roleManager.AddClaimAsync(role, new Claim(permission, EAllowType.Allow.ToString()));
                }
            }
        }
    }
}
