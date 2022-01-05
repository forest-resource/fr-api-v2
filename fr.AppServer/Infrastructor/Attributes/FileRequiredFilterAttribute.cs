using fr.Core.Exceptions;
using fr.Core.Extensions;
using fr.Service.FileService;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace fr.AppServer.Infrastructor.Attributes;

public class FileRequiredFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var fileService = context.HttpContext.RequestServices.GetService<IFileService>();
        if (fileService != null)
        {
            if (context.HttpContext.Request.Form.Files?.Any() ?? false)
            {
                fileService.File = context.HttpContext.Request.Form.Files[0];
                return;
            }
            throw new EntityNotFoundException("File not found");
        }

        throw new AppException("File Service not found");
    }
}
