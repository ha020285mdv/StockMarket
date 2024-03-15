using StockMarket.Constants;
using StockMarket.Database;
using StockMarket.Database.Model;
using StockMarket.Services;
using System.Net;
using System.Security.Claims;

namespace StockMarket.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUserService _userService;

        public ApiKeyMiddleware(
            RequestDelegate next,
            IServiceProvider serviceProvider
        )
        {
            _next = next;
            _userService = serviceProvider.GetRequiredService<IUserService>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // let all POST requests in "/api/v1/users" endpoint
            if (context.Request.Path.StartsWithSegments("/api/v1/users") && context.Request.Method == "POST")
            {
                await _next(context);
                return;
            }

            // stop all requests without X-API-Key
            string? userApiKey = context.Request.Headers["X-API-Key"];
            if (string.IsNullOrWhiteSpace(userApiKey))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            // get the user from the database
            UserEntity? User = await _userService.GetUserAsync(userApiKey);

            // check if user exists, if not - stop the request
            if (User == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }

            // check if email from GET request belongs to the user, if not - stop the request
            if (context.Request.Path.StartsWithSegments("/api/v1/users") && context.Request.Method == "GET")          
            {
                // TODO: how to get the email from the request query without Split()?
                string? requestEmail = context.Request.Path.Value.Split('/').LastOrDefault(); 
                if (requestEmail != User.Email)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return;
                }
            }

            ClaimsPrincipal principal = new ClaimsPrincipal();

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(CustomClaimType.UserId, User.Id.ToString()));
            claims.Add(new Claim(CustomClaimType.Name, User.Name));
            claims.Add(new Claim(CustomClaimType.Email, User.Email));

            ClaimsIdentity identity = new ClaimsIdentity(claims, "Custom");

            principal.AddIdentity(identity);

            context.User = principal;

            await _next(context);
        }
    }
}
