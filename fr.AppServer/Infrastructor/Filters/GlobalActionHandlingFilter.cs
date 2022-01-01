using fr.AppServer.Models;
using fr.Database.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace fr.AppServer.Infrastructor.Filters
{
    public class GlobalActionHandlingFilter : IActionFilter
    {
        private readonly ILogger logger;
        private readonly IAuditService auditService;

        public GlobalActionHandlingFilter(ILoggerFactory loggerFactory, IAuditService auditService)
        {
            logger = loggerFactory.CreateLogger<GlobalActionHandlingFilter>();
            this.auditService = auditService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult && objectResult.Value is not SuccessResponseModel)
            {
                context.Result = new JsonResult(new SuccessResponseModel
                {
                    Status = 200,
                    Data = objectResult.Value
                });
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            auditService.UserName = context.HttpContext.User.Identity.Name ?? "Anonymous";
            logger.LogInformation($"Action '{context.ActionDescriptor.DisplayName}' executing");
        }
    }
}
