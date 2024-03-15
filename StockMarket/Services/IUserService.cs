using Azure.Core;
using StockMarket.Database.Model;
using System;

namespace StockMarket.Services;

public interface IUserService
{
    public UserSession? GetUserSession();

    public Task<UserEntity?> GetUserByXAPIAsync(string XAPIKey, CancellationToken? cancellationToken);

    public Task<UserEntity?> GetUserByEmailAsync(string XAPIKey, CancellationToken? cancellationToken);

    public Task<UserEntity?> CreateUserAsync(string name, string email, string XAPIKey, decimal funds, CancellationToken? cancellationToken);
}

public class UserSession
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}