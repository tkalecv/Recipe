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
                await CreateContextResponse(context, StatusCodes.Status401Unauthorized, ex.Message);
            }
            catch (Firebase.Auth.FirebaseAuthException ex)
            {
                JObject responseData = JObject.Parse(ex.ResponseData);
                string message = responseData.SelectToken("error.message").ToString();
                int code = (int)responseData.SelectToken("error.code");

                await CreateContextResponse(context, code, message);
            }
            catch (FirebaseAdmin.Auth.FirebaseAuthException ex)
            {
                int code = int.Parse(Regex.Match(ex.HttpResponse.ToString(), @"(?<=StatusCode:.+?).+?(?=,)").Value);
                string message = Regex.Match(ex.Message.ToString(), "(?<=\"message\": \".+?).+?(?=\",)").Value;

                await CreateContextResponse(context, code, message);
            }
            catch (ArgumentException ex)
            {
                await CreateContextResponse(context, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception exceptionObj)
            {
                await HandleExceptionAsync(context, exceptionObj);
            }
        }

        /// <summary>
        /// Method creates error response
        /// </summary>
        /// <param name="context">HttpContext object</param>
        /// <param name="statusCode">Status code that will be returned</param>
        /// <param name="message">Error message that will be returned</param>
        /// <param name="contentType">Content type response header</param>
        /// <returns>Task</returns>
        private Task CreateContextResponse(HttpContext context, int statusCode, string message, string contentType = "application/json")
        {
            string result = null;
            context.Response.ContentType = contentType;

            result = new ErrorDetails()
            {
                Message = message,
                StatusCode = statusCode
            }.ToString();

            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(result);
        }

        /// <summary>
        /// Method handles custom HttpStatusCodeException exceptions
        /// </summary>
        /// <param name="context">HttpContext object</param>
        /// <param name="exception">HttpStatusCodeException object</param>
        /// <returns>Task</returns>
        private Task HandleExceptionAsync(HttpContext context, HttpStatusCodeException exception)
        {

            if (exception is HttpStatusCodeException)
            {
                return CreateContextResponse(context, (int)exception.StatusCode, exception.Message);
            }
            else
            {
                return CreateContextResponse(context, StatusCodes.Status400BadRequest, "Runtime Error");
            }
        }

        /// <summary>
        /// Method handles global exceptions
        /// </summary>
        /// <param name="context">HttpContext object</param>
        /// <param name="exception">Exception object</param>
        /// <returns>Task</returns>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            return CreateContextResponse(context, StatusCodes.Status500InternalServerError, exception.Message);
        }
    }
}

