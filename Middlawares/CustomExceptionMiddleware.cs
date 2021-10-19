using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using BookStoreWebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BookStoreWebApi.Middlawares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;
        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
               
                string message = "[Request] HTTP " + context.Request.Method + "-" + context.Request.Path;
                _loggerService.Write(message);
                await _next(context); // bir sonraki middleware çağırıldı
                watch.Stop();
                message = "[Responsa] HTTP " +
                          context.Request.Method + "-" + 
                          context.Request.Path + " responded " +
                          context.Response.StatusCode + " in " +watch.Elapsed.TotalMilliseconds+" ms";
                _loggerService.Write(message);
            }
            catch (Exception e)
            {
                watch.Stop();
                await HandleException(context, e, watch);
            }
            
            
         
            

        }

        private Task HandleException(HttpContext context, Exception e, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            string message = "[Error] HTTP " +
                             context.Request.Method + "-" +
                             context.Response.StatusCode +
                             " Error Message: " + e.Message + " in " +
                             watch.Elapsed.TotalMilliseconds + " ms";
            _loggerService.Write(message);
           

            var result = JsonConvert.SerializeObject(new {error = e.Message},Formatting.None);

            return context.Response.WriteAsync(result); 

        }
    }

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}