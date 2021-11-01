using System;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LitBlog.API.Helpers
{
    public static class IdContext
    {
        public static int GetUserId(HttpContext httpContext)
        {
            int id = 0;
            if (httpContext.User.Identity is ClaimsIdentity identity)
                int.TryParse(identity.FindFirst("Id")?.Value, out id);
            if (id == 0)
                throw new UnauthorizedAccessException();
            return id;
        }

    }
}
