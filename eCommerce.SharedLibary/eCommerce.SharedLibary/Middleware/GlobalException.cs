using eCommerce.SharedLibary.Logs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eCommerce.SharedLibary.Middleware
{
    public class GlobalException(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context)
        {
            string message = "Internal server errror";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string title = "error";
            try
            {
                await next(context);
                if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
                {
                    title = "Warning";
                    message = "Too many request made.";
                    statusCode = (int)HttpStatusCode.TooManyRequests;
                    await ModifyHeader(context, title, message, statusCode);
                }
                if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    title = "Alert";
                    message = "You are not authorized to access.";
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    await ModifyHeader(context, title, message, statusCode);
                }
                if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
                {
                    title = "Out of access";
                    message = "You are not allowed/required to access.";
                    statusCode = (int)HttpStatusCode.Forbidden;
                    await ModifyHeader(context, title, message, statusCode);
                }
            }
            catch (Exception ex)
            {   
                LogException.LogExceptions(ex);
                // check if exception is timeout // 408
                if (ex is TaskCanceledException || ex is TimeoutException) 
                {
                    title = "Out of time";
                    message = "Request Timeout ... try again "; 
                    statusCode = StatusCodes.Status408RequestTimeout;

                }
                await ModifyHeader(context, title, message, statusCode);
            }
        }

        private async Task ModifyHeader(HttpContext context, string title, string message, int statusCode)
        {
            context.Response.ContentType = "application/json";
            var json = JsonSerializer.Serialize(new Microsoft.AspNetCore.Mvc.ProblemDetails() 
            {
             Detail = message,
             Title = title,
             Status = statusCode
            });
            await context.Response.WriteAsync(json , CancellationToken.None); return; 
        }
    }
}
