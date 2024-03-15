using Microsoft.EntityFrameworkCore;
using StockMarket.Constants;
using StockMarket.Database;
using StockMarket.Database.Model;

namespace StockMarket.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IServiceProvider _serviceProvider;

    public UserService(
        IHttpContextAccessor httpContextAccessor,
        IServiceProvider serviceProvider)
    {
        _httpContextAccessor = httpContextAccessor;
        _serviceProvider = serviceProvider;
    }

    public async Task<UserEntity?> GetUserAsync(string XAPIKey)
    {
        // get access to the database
        using var scope = _serviceProvider.CreateScope();
        DatabaseContext database = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        // return the user if found
        return await database.Users.FirstOrDefaultAsync(u => u.XAPIKey == XAPIKey);
    }

    public UserSession? GetUserSession()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null) {
            return null;
        }

        string userId = user.Claims.First(c => c.Type == CustomClaimType.UserId).Value;
        string name = user.Claims.First(c => c.Type == CustomClaimType.Name).Value;
        string email = user.Claims.First(c => c.Type == CustomClaimType.Email).Value;

        return new UserSession() { 
            Id = Guid.Parse(userId), 
            Email = email, 
            Name = name 
        };
    }
}
