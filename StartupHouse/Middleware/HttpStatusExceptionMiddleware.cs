using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace StartupHouse.API.Middleware
{
    public class HttpStatusExceptionMiddleware
    {
        private readonly RequestDelegate _request;

        public HttpStatusExceptionMiddleware(RequestDelegate request)
        {
            _request = request;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _request(httpContext);
            }
            catch (InvalidOperationException)
            {
                HandleInvalidOperationException(httpContext);
            }
            catch
            {
                HandleException(httpContext);
                throw;
            }
        }

        private void HandleInvalidOperationException(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }

        private void HandleException(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //TODO: Log exceptions.
        }
    }
}
