using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PAC.WebAPI.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                // Si asigno un result se corta la ejecucion de la request y ya devuelve la response
                context.Result = new ObjectResult(new { Message = "Authorization header is missing" })
                {
                    StatusCode = 401
                };
            }
        }

    }
}

