﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZPI_Database.DataAccess;

namespace Backend.Middleware
{
    public class ValidateAuthorization
    {
        private readonly RequestDelegate _next;

        public ValidateAuthorization(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ZPIContext _context)
        {
            var url = httpContext.Request.Path.ToString();
            var loginToken = httpContext.Request.Headers["Authorization"].ToString();
            if (!_context.Users.Any(u => "Bearer "+u.Token == loginToken) && !url.Equals("/api/Account/login"))
            {
                httpContext.Response.StatusCode = 403;
            }
            else
            {
                await _next.Invoke(httpContext);
            }
        }
    }
}
