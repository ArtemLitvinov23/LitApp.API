using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace LitChat.API.Helpers
{
    public static class UserContext
    {
        public static int GetUserId(HttpContext httpContext)
        {
            int id = 0;
            if (httpContext.User.Identity is ClaimsIdentity identity)
            {
                int.TryParse(identity.FindFirst("Id")?.Value, out id);
            }

            if (id == 0)
            {
                throw new ApplicationException("User is not found");
            }

            return id;
        }

    }
}
