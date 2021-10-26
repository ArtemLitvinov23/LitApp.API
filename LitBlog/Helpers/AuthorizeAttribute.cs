using LitBlog.BLL.ModelsDto;
using LitBlog.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using LitBlog.API.Models;

namespace LitBlog.API.Helpers
{
    public class AuthorizeAttribute: Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> _roles;

        public AuthorizeAttribute(params Role[] roles)
        {
            _roles = roles ?? new Role[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var account = (Account) context.HttpContext.Items["Account"];
            if (account == null || (_roles.Any() && !_roles.Contains(account.Role)))
            {
                context.Result = new JsonResult(new {message = "Unauthorized"})
                    {StatusCode = StatusCodes.Status401Unauthorized};
            }
        }
    }
}
