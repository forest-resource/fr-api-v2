using fr.AppServer.Models;
using fr.Core;
using fr.Service.jwtService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace fr.AppServer.Infrastructor.Filters
{
    public class GlobalAuthorizationHandlingFilter : IAuthorizationFilter
    {
        private readonly IJwtService jwtService;
        public GlobalAuthorizationHandlingFilter(IJwtService jwtService)
        {
            this.jwtService = jwtService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var actionFilters = context.ActionDescriptor.FilterDescriptors;
            if (actionFilters.Any(r => r.Filter is AllowAnonymousFilter))
            {
                return;
            }

            var authorizationHeader = context.HttpContext.Request.Headers.Authorization;
            if (string.IsNullOrWhiteSpace(authorizationHeader))
            {
                context.Result = new OkObjectResult(
                    new ErrorResponseModel
                    {
                        Status = 401,
                        Code = "UNAUTHORIZE",
                        Message = "Unauthorize"
                    });
                return;
            }
            var authorizationArray = authorizationHeader.ToString().Split(' ');
            var schema = authorizationArray[0];

            if (schema.ToLower() != Constants.DefaultSchema.ToLower())
            {
                context.Result = new OkObjectResult(
                    new ErrorResponseModel
                    {
                        Status = 401,
                        Code = "UNAUTHORIZE",
                        Message = "Unauthorize"
                    });
                return;
            }

            var token = authorizationArray[1];
            var tokenValidationResult = jwtService.ValidateToken(token);

            if (!tokenValidationResult.IsValid)
            {
                context.Result = new OkObjectResult(
                    new ErrorResponseModel
                    {
                        Status = 401,
                        Code = "UNAUTHORIZE",
                        Message = "Unauthorize"
                    });
                return;
            }

            context.HttpContext.User = new ClaimsPrincipal(tokenValidationResult.ClaimsIdentity);
        }
    }
}
