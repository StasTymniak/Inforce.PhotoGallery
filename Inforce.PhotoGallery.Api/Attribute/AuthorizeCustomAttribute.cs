using Inforce.PhotoGallery.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Inforce.PhotoGallery.Api.Attribute
{
    public class AuthorizeCustomAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var tokenBlacklistService = context.HttpContext.RequestServices.GetRequiredService<ITokenBlacklistService>();
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                var token = authorizationHeader.Replace("Bearer ", "");

                if (tokenBlacklistService.IsTokenBlacklisted(token))
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }
    }
}
