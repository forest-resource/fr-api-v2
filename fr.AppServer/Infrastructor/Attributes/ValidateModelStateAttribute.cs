using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace fr.AppServer.Infrastructor.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //if (context.ModelState.IsValid)
            //{
            //    return;
            //}

            //throw new ValidationException(context.ModelState.GetErrors());
        }
    }
}
