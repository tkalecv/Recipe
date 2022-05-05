using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Recipe.ExceptionHandler.CustomExceptions;
using Recipe.ExceptionHandler.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Recipe.ExceptionHandler
{
    public class CustomExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (HttpStatusCodeException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception exceptionObj)
            {
                await HandleExceptionAsync(context, exceptionObj);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, HttpStatusCodeException exception)
        {
            string result = null;
            context.Response.ContentType = "application/json";
            if (exception is HttpStatusCodeException)
            {
                result = new ErrorDetails()
                {
                    Message = exception.Message,
                    StatusCode = (int)exception.StatusCode
                }.ToString();
                context.Response.StatusCode = (int)exception.StatusCode;
            }
            else
            {
                result = new ErrorDetails()
                {
                    Message = "Runtime Error",
                    StatusCode = StatusCodes.Status400BadRequest
                }.ToString();
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            return context.Response.WriteAsync(result);
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string result = new ErrorDetails()
            {
                Message = exception.Message,
                StatusCode = StatusCodes.Status500InternalServerError
            }.ToString();
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            return context.Response.WriteAsync(result);
        }
    }
}

