using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Authorization {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter 
    {
        private AccessLevel[] AccessLevels;

        public AuthorizeAttribute(params AccessLevel[] accessLevels)
        {
            AccessLevels = accessLevels;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var level = (AccessLevel)context.HttpContext.Items["AccessLevel"];
            if (!AccessLevels.Contains(level)) {
                context.Result = new ForbidResult();
            }
        }
    }
}