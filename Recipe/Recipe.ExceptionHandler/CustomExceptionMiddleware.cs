using Firebase.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Recipe.ExceptionHandler.CustomExceptions;
using Recipe.ExceptionHandler.Models;
using System;
using System.Net;
using System.Text.RegularExpressions;
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
            catch (UnauthorizedAccessException ex)
            {
                string result = null;
                context.Response.ContentType = "application/json";

                result = new ErrorDetails()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status401Unauthorized
                }.ToString();

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsync(result);
            }
            catch (Firebase.Auth.FirebaseAuthException ex)
            {
                JObject responseData = JObject.Parse(ex.ResponseData);
                string message = responseData.SelectToken("error.message").ToString();
                int code = (int)responseData.SelectToken("error.code");

                string result = null;
                context.Response.ContentType = "application/json";

                result = new ErrorDetails()
                {
                    Message = message,
                    StatusCode = code
                }.ToString();

                context.Response.StatusCode = code;

                await context.Response.WriteAsync(result);
            }
            catch (FirebaseAdmin.Auth.FirebaseAuthException ex)
            {
                string result = null;
                context.Response.ContentType = "application/json";
                int code = int.Parse(Regex.Match(ex.HttpResponse.ToString(), @"(?<=StatusCode:.+?).+?(?=,)").Value);

                result = new ErrorDetails()
                {
                    Message = ex.Message,
                    StatusCode = code
                }.ToString();

                context.Response.StatusCode = code;

                await context.Response.WriteAsync(result);
            }
            catch (ArgumentException ex)
            {
                string result = null;
                context.Response.ContentType = "application/json";
                int code = StatusCodes.Status400BadRequest;

                result = new ErrorDetails()
                {
                    Message = ex.Message,
                    StatusCode = code
                }.ToString();

                context.Response.StatusCode = code;

                await context.Response.WriteAsync(result);

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

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            return context.Response.WriteAsync(result);
        }
    }
}

