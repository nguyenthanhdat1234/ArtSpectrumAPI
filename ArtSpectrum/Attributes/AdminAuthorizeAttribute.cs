using ArtSpectrum.Repository.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ArtSpectrum.Attributes
{
    public class AdminAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated || !user.IsInRole("Admin", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new JsonResult(new
                {
                    message = "Admin Only"
                })
                {
                    StatusCode = 403 // Forbidden status code
                };

                await Task.CompletedTask;
            }
        }
    }
}
