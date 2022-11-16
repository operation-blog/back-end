using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using GF.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly IList<Role> _roles;

    public AuthorizeAttribute(params Role[] roles) { _roles = roles ?? new Role[] { }; }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = (User)context.HttpContext.Items["User"];

        if (user == null || (_roles.Any() && !_roles.Contains(user.Role)))
            context.Result = new JsonResult(new { status = 0,  message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
    }
}