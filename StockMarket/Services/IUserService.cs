using StockMarket.Database.Model;

namespace StockMarket.Services;

public interface IUserService
{
    public UserSession? GetUserSession();

    Task<UserEntity?> GetUserAsync(string XAPIKey);
}

public class UserSession
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}