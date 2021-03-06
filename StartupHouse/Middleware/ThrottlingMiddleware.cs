﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net;
using System.Threading.Tasks;

namespace StartupHouse.API.Middleware
{
    public class ThrottlingMiddleware
    {
        private readonly RequestDelegate _next;
        private IMemoryCache _cache;

        public ThrottlingMiddleware(RequestDelegate next, IMemoryCache memoryCache)
        {
            _next = next;
            _cache = memoryCache;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.Request.Path.ToString().ToLower().Contains("api"))
            {
                string ip = httpContext.Connection.RemoteIpAddress.ToString();

                if (_cache.TryGetValue(ip, out _))
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    return;
                }

                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                options.AbsoluteExpiration = DateTime.Now.AddSeconds(1);

                _cache.Set(ip, DateTime.Now.ToString(), options);
            }

            await _next(httpContext);
        }
    }
}
