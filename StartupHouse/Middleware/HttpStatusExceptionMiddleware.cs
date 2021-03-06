﻿using Microsoft.AspNetCore.Http;
using StartupHouse.API.Interfaces.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace StartupHouse.API.Middleware
{
    public class HttpStatusExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpStatusExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (InvalidOperationException ex)
            {
                await HandleInvalidOperationException(httpContext, ex);
            }
            catch (NotFoundException ex)
            {
                await HandleNotFoundException(httpContext, ex);

            }
            catch (NbpApiException ex)
            {
                await HandleNbpApiException(httpContext, ex);

            }
            catch
            {
                HandleException(httpContext);
                throw;
            }
        }

        private async Task HandleInvalidOperationException(HttpContext context, InvalidOperationException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(ex.Message);
        }

        private async Task HandleNotFoundException(HttpContext context, NotFoundException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsync(ex.Message);
        }

        private async Task HandleNbpApiException(HttpContext context, NbpApiException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
            await context.Response.WriteAsync(ex.Message);
        }

        private void HandleException(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //TODO: Log exceptions.
        }
    }
}
