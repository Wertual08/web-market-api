using System;
using System.Linq;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Authorization {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter 
    {
        private UserRoleId[] UserRoles;

        public AuthorizeAttribute(params UserRoleId[] accessLevel)
        {
            UserRoles = accessLevel;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Items["AccessLevel"] is null) {
                context.Result = new ForbidResult();
            } else {
                var level = (UserRoleId)context.HttpContext.Items["AccessLevel"];
                if (UserRoles.Length > 0 && !UserRoles.Contains(level)) {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}