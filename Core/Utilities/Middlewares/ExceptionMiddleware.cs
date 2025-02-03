using Core.Utilities.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Utilities.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode;
            string message;

            if (exception is CustomExceptionBase customException)
            {
                statusCode = (int)customException.StatusCode;
                message = customException.CustomMessage;
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
                message =  $"Bilinmeyen hata: {exception.Message} - {exception.StackTrace}";
            }

            var response = new
            {
                StatusCode = statusCode,
                Message = message
            };

            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
