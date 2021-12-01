using fr.AppServer.Models;
using fr.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;

namespace fr.AppServer.Infrastructor.Filters
{
    public class GlobalExceptionHandlingFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var responseModel = HandleExceptionAsync(context.Exception);
            context.Result = new JsonResult(responseModel);

            context.ExceptionHandled = true;
        }

        private static ErrorResponseModel HandleExceptionAsync(Exception exception) => exception switch
        {
            AppException e => GetErrorResponseModel(e.Status, e.Message, e.Data),
            SqlException e => GetErrorResponseModel(400, e.Message, e.Errors),
            Exception e => GetErrorResponseModel(400, e.Message, e.Data)
        };

        private static ErrorResponseModel GetErrorResponseModel(int status, string message, ICollection errors)
        {
            var errorList = errors?.OfType<object>().Select(JsonConvert.SerializeObject) ?? null;

            return new()
            {
                Status = status,
                
                Message = message,
                Errors = errorList?.Select(k => new ErrorModel { Message = k }).ToArray() ?? null
            };
        }
    }
}
