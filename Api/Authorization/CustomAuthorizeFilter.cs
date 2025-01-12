using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security;


namespace Api.Authorization
{
    public class CustomAuthorizeFilter : IAsyncAuthorizationFilter
    {
        private readonly IMediator _mediator;

        public CustomAuthorizeFilter(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = (bool)context.HttpContext.Items["IsAnonymous"];
            if (allowAnonymous)
                return;

            // authorization

            if (context.HttpContext.Items["UserId"] == null)
            {
                // not logged in or role not authorized
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {
#pragma warning disable CS8605 // Unboxing a possibly null value.
                var currentUserId = (int)context.HttpContext.Items["UserId"];

            }

        }
    }
}
