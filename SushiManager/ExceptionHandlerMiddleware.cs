using SushiRestaurant.Models;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;

namespace SushiRestaurant
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            var current_url = context.Request.Path + context.Request.QueryString;

            if (context.Response.StatusCode == 401)
            {                
                // Redirect to the Unauthorized page                                
                context.Response.Redirect($"/UserLogin/Login?ReturnUrl={current_url}");
                return;
            }
            else if (context.Response.StatusCode == 403)
            {
                // Redirect to the Forbidden page
                var error_message = "Non si dispongono dei diritti per effettuare questa operazione!";
                context.Response.Redirect($"/Home/Index?message={error_message}");

                return;
            }            
        }
    }
}
