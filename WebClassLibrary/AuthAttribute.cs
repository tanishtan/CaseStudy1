﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClassLibrary
{
    public class AuthAttribute : Attribute//, IAuthorizationFilter
    {
        public string? _rolesString;
        private string[] allowedRoles;

        public AuthAttribute(string? roles)
        {
            allowedRoles = new string[1];
            if (!string.IsNullOrEmpty(roles))
            {
                _rolesString = roles;
                allowedRoles = roles.Split(',');
            }

        }
        /*public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["User"] as UserModel;
            if (user is not null)
            {
                var role = user.RoleName.ToLower();
                if (!allowedRoles.ToList().Exists(c => c == role))
                {
                    context.Result = new JsonResult(
                    new { message = "You are not authorized to use this APIs. Contact your admin." }
                    )
                    {
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                }
            }
            else
            {
                context.Result = new JsonResult(
                    new { message = "You are not authorized to use this APIs. Contact your admin." }
                    )
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }*/
    }
}
