using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Donate.Shared.API.Middleware
{
    public class HttpStatusCodeExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HttpStatusCodeExceptionMiddleware> _logger;

        public HttpStatusCodeExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory?.CreateLogger<HttpStatusCodeExceptionMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                var errorResponse = new ErrorResponse { StatusCode = (int) HttpStatusCode.BadRequest };
                errorResponse.AddError(ex.Message);

                context.Response.Clear();
                context.Response.StatusCode = errorResponse.StatusCode;
                context.Response.ContentType = @"application/json";

                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));

                return;
            }
        }
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }

        public ErrorResponse()
        {
            Errors = new List<string>();
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }
    }
}
