using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NuGet.Protocol;
using System.ComponentModel;
using webapinews.Entities;
using webapinews.Interface;

namespace webapinews.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private readonly string[] _roles;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings, string[] roles = null)
        {
            _next = next;
            _appSettings = appSettings.Value;
            _roles = roles;
    }

        public async Task Invoke(HttpContext context, IUserReporistory userReporistory, IJwtAuth jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                context.Items["User"] = userReporistory.GetById(userId.Value);
            }
            await _next(context);
        }
    }

}

