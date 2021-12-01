using fr.AppServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace fr.AppServer.Infrastructor.Filters
{
    public class GlobalActionHandlingFilter : IActionFilter
    {
        private readonly ILogger logger;

        public GlobalActionHandlingFilter(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<GlobalActionHandlingFilter>();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is not ObjectResult objectResult || objectResult.Value is SuccessResponseModel)
            {
                return;
            }

            context.Result = new JsonResult(new SuccessResponseModel
            {
                Status = 200,
                Data = objectResult.Value
            });
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogInformation($"Action '{context.ActionDescriptor.DisplayName}' executing");
        }
    }
}
