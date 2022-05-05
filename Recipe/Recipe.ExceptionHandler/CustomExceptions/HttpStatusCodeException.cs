using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace Recipe.ExceptionHandler.CustomExceptions
{
    public class HttpStatusCodeException : Exception
    {
        public int StatusCode { get; set; }
        public string ContentType { get; set; } = @"text/plain";

        public HttpStatusCodeException(int statusCode)
            : base(message: GetDefaultMessageForStatusCode(statusCode))
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCodeException(int statusCode, string message)
            : base(message)
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCodeException(int statusCode, Exception inner)
            : this(statusCode, inner.ToString()) { }

        public HttpStatusCodeException(int statusCode, object errorObject) //JObject errorObject
            : this(statusCode, errorObject.ToString())
        {
            this.ContentType = @"application/json";
        }

        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case StatusCodes.Status404NotFound:
                    return "Resource not found";
                case StatusCodes.Status500InternalServerError:
                    return "An unhandled error occurred";
                case StatusCodes.Status400BadRequest:
                    return "Runtime Error";
                default:
                    return null;
            }
        }
    }
}
