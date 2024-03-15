using Microsoft.EntityFrameworkCore;
using StockMarket.Constants;
using StockMarket.Database;
using StockMarket.Database.Model;

namespace StockMarket.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DatabaseContext _database;

    public UserService(
        IHttpContextAccessor httpContextAccessor,
        DatabaseContext database)
    {
        _httpContextAccessor = httpContextAccessor;
        _database = database;
    }

    public async Task<UserEntity?> GetUserByXAPIAsync(string XAPIKey, CancellationToken? cancellationToken)
    {
        return await _database.Users.FirstOrDefaultAsync(u => u.XAPIKey == XAPIKey);
    }

    public async Task<UserEntity?> GetUserByEmailAsync(string email, CancellationToken? cancellationToken)
    {
        return await _database.Users.FirstOrDefaultAsync(u => u.Email == email.ToLower());
    }

    public async Task<UserEntity?> CreateUserAsync(string name, string email, string XAPIKey, decimal funds, CancellationToken? cancellationToken)
    {
        var entity = new UserEntity
        {
            Name = name,
            Email = email,
            XAPIKey = XAPIKey,
            Funds = funds
        };

        _database.Users.Add(entity);
        await _database.SaveChangesAsync(cancellationToken ?? CancellationToken.None);

        return entity;
    }

    public UserSession? GetUserSession()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
        {
            return null;
        }

        string userId = user.Claims.First(c => c.Type == CustomClaimType.UserId).Value;
        string name = user.Claims.First(c => c.Type == CustomClaimType.Name).Value;
        string email = user.Claims.First(c => c.Type == CustomClaimType.Email).Value;

        return new UserSession()
        {
            Id = Guid.Parse(userId),
            Email = email,
            Name = name
        };
    }
}
