using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using StockMarket.Database;
using StockMarket.Database.Model;
using System.Net;
using System.Security.Claims;

namespace StockMarket.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public ApiKeyMiddleware(
            RequestDelegate next,
            IServiceProvider serviceProvider
        )
        {
            _next = next;
            _serviceProvider = serviceProvider;
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

            // get access to the database
            using var scope = _serviceProvider.CreateScope();
            DatabaseContext database = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

            // check if the user exists
            UserEntity? User = await database.Users.FirstOrDefaultAsync(u => u.XAPIKey == userApiKey);
            if (User == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }

            // check if email from GET request belongs to the user, if not - stop the request
            // TODO: how to make it prettier?
            if (context.Request.Path.StartsWithSegments("/api/v1/users") && context.Request.Method == "GET")          
            {
                string? requestEmail = context.Request.Path.Value.Split('/').LastOrDefault();
                if (requestEmail != User.Email)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return;
                }
            }

            ClaimsPrincipal principal = new ClaimsPrincipal();

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("UserId", User.Id.ToString()));
            claims.Add(new Claim("Name", User.Name));
            claims.Add(new Claim("Email", User.Email));

            ClaimsIdentity identity = new ClaimsIdentity(claims);

            principal.AddIdentity(identity);

            context.User = principal;

            Console.WriteLine("User authenticated: " + User.Name);

            await _next(context);
        }
    }
}
