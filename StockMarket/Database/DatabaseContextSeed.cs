using Microsoft.EntityFrameworkCore;
using Polly.Retry;
using Polly;
using StockMarket.Database;
using StockMarket.Database.Model;
using StockMarket.Application.Commands;

namespace StockMarket.Database;

public class DatabaseContextSeed
{
    private readonly DatabaseContext _context;
    private readonly ILogger<DatabaseContextSeed> _logger;

    public DatabaseContextSeed(DatabaseContext context, ILogger<DatabaseContextSeed> logger)
    {
        _context = context;
        _logger = logger;
    }

    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        var logger = services.GetRequiredService<ILogger<DatabaseContextSeed>>();

        var seeder = new DatabaseContextSeed(context, logger);

        var policy = seeder.RetryPolicy();
        await policy.ExecuteAsync(seeder.TrySeedAsync);
    }

    private async Task TrySeedAsync()
    {
        _logger.LogInformation("Migrating database");
        await _context.Database.MigrateAsync();

        _logger.LogInformation("Seeding database");
        await SeedSecurities();
        var users = await SeedUsers();
        await SeedPortfolios(users);
     }

    private async Task SeedSecurities()
    {
        if (_context.Securities.Any())
            return;

        _context.Securities.Add(new SecurityEntity() { Ticker = "AAPL", Name = "Apple Inc." });
        _context.Securities.Add(new SecurityEntity() { Ticker = "MSFT", Name = "Microsoft Corporation" });
        _context.Securities.Add(new SecurityEntity() { Ticker = "AMZN", Name = "Amazon.com Inc." });
        _context.Securities.Add(new SecurityEntity() { Ticker = "GOOGL", Name = "Alphabet Inc." });
        _context.Securities.Add(new SecurityEntity() { Ticker = "META", Name = "Meta Platforms, Inc." });
        _context.Securities.Add(new SecurityEntity() { Ticker = "TSLA", Name = "Tesla, Inc." });
        _context.Securities.Add(new SecurityEntity() { Ticker = "V", Name = "Visa Inc." });
        _context.Securities.Add(new SecurityEntity() { Ticker = "JNJ", Name = "Johnson & Johnson" });
        _context.Securities.Add(new SecurityEntity() { Ticker = "FB", Name = "Facebook, Inc." });
        _context.Securities.Add(new SecurityEntity() { Ticker = "WMT", Name = "Walmart Inc." });
        _context.Securities.Add(new SecurityEntity() { Ticker = "PG", Name = "Procter & Gamble Company" });
        _context.Securities.Add(new SecurityEntity() { Ticker = "MA", Name = "Mastercard Incorporated" });
        _context.Securities.Add(new SecurityEntity() { Ticker = "NVDA", Name = "Nvidia Corporation" });
        _context.Securities.Add(new SecurityEntity() { Ticker = "DIS", Name = "Walt Disney Company" });
        _context.Securities.Add(new SecurityEntity() { Ticker = "BAC", Name = "Bank of America Corporation" });
        _context.Securities.Add(new SecurityEntity() { Ticker = "PYPL", Name = "PayPal Holdings, Inc." });
        await _context.SaveChangesAsync();
    }

    private async Task<List<UserEntity>> SeedUsers()
    {
        List<UserEntity> users = await _context.Users.ToListAsync();

        if (users.Any())
            return users;

        users = new List<UserEntity>() {
            new UserEntity() { Name = "John Doe", Email = "john.doe@example.com", XAPIKey = CreateUserCommandHandler.GenerateSafeRandomString(30), Funds = GetRandomDecimal() },
            new UserEntity() { Name = "Jane Smith", Email = "jane.smith@example.com", XAPIKey = CreateUserCommandHandler.GenerateSafeRandomString(30), Funds = GetRandomDecimal() },
            new UserEntity() { Name = "Michael Johnson", Email = "michael.johnson@example.com", XAPIKey = CreateUserCommandHandler.GenerateSafeRandomString(30), Funds = GetRandomDecimal() },
            new UserEntity() { Name = "Emily Brown", Email = "emily.brown@example.com", XAPIKey = CreateUserCommandHandler.GenerateSafeRandomString(30), Funds = GetRandomDecimal() },
            new UserEntity() { Name = "William Taylor", Email = "william.taylor@example.com", XAPIKey = CreateUserCommandHandler.GenerateSafeRandomString(30), Funds = GetRandomDecimal() },
            new UserEntity() { Name = "Sophia Martinez", Email = "sophia.martinez@example.com", XAPIKey = CreateUserCommandHandler.GenerateSafeRandomString(30), Funds = GetRandomDecimal() },
            new UserEntity() { Name = "James Anderson", Email = "james.anderson@example.com", XAPIKey = CreateUserCommandHandler.GenerateSafeRandomString(30), Funds = GetRandomDecimal() },
            new UserEntity() { Name = "Olivia Garcia", Email = "olivia.garcia@example.com", XAPIKey = CreateUserCommandHandler.GenerateSafeRandomString(30), Funds = GetRandomDecimal() },
            new UserEntity() { Name = "Benjamin Hernandez", Email = "benjamin.hernandez@example.com", XAPIKey = CreateUserCommandHandler.GenerateSafeRandomString(30), Funds = GetRandomDecimal() },
            new UserEntity() { Name = "Emma Smith", Email = "emma.smith@example.com", XAPIKey = CreateUserCommandHandler.GenerateSafeRandomString(30), Funds = GetRandomDecimal() },
        };

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        return users;
    }

    private async Task SeedPortfolios(List<UserEntity> users)
    {
        if (_context.UsersPortfolios.Any())
            return;

        //var user1 = users.Find(u => u.Name == "John Doe");
        _context.UsersPortfolios.Add(new UserPortfolioEntity() { Number = GetRandomInt(), UserId = users[GetRandomInt(0, users.Count-1)].Id, Ticker = "GOOGL" });
        _context.UsersPortfolios.Add(new UserPortfolioEntity() { Number = GetRandomInt(), UserId = users[GetRandomInt(0, users.Count - 1)].Id, Ticker = "GOOGL" });
        _context.UsersPortfolios.Add(new UserPortfolioEntity() { Number = GetRandomInt(), UserId = users[GetRandomInt(0, users.Count - 1)].Id, Ticker = "GOOGL" });
        _context.UsersPortfolios.Add(new UserPortfolioEntity() { Number = GetRandomInt(), UserId = users[GetRandomInt(0, users.Count - 1)].Id, Ticker = "AAPL" });
        _context.UsersPortfolios.Add(new UserPortfolioEntity() { Number = GetRandomInt(), UserId = users[GetRandomInt(0, users.Count - 1)].Id, Ticker = "AAPL" });
        _context.UsersPortfolios.Add(new UserPortfolioEntity() { Number = GetRandomInt(), UserId = users[GetRandomInt(0, users.Count - 1)].Id, Ticker = "AAPL" });
        _context.UsersPortfolios.Add(new UserPortfolioEntity() { Number = GetRandomInt(), UserId = users[GetRandomInt(0, users.Count - 1)].Id, Ticker = "META" });
        _context.UsersPortfolios.Add(new UserPortfolioEntity() { Number = GetRandomInt(), UserId = users[GetRandomInt(0, users.Count - 1)].Id, Ticker = "META" });
        _context.UsersPortfolios.Add(new UserPortfolioEntity() { Number = GetRandomInt(), UserId = users[GetRandomInt(0, users.Count - 1)].Id, Ticker = "META" });
        await _context.SaveChangesAsync();
    }

    private AsyncRetryPolicy RetryPolicy()
    {
        return Policy.Handle<Exception>().WaitAndRetryAsync(
            retryCount: 3,
            sleepDurationProvider: count => TimeSpan.FromSeconds(Math.Pow(2, count)),
            onRetry: OnRetry
        );
    }

    private void OnRetry(Exception exception, TimeSpan timeSpan, int retry, Context ctx)
    {
        _logger.LogWarning(
            exception,
            "Database migration or seeding failed on attempt {Retry} of {RetryCount}, sleeping for {TimeSpan}",
            retry,
            3,
            timeSpan
        );
    }

    private static int GetRandomInt(int min=1, int max=1000)
    {
        Random random = new Random();
        return random.Next(min, max + 1);
    }

    private static decimal GetRandomDecimal(decimal min=1000, decimal max=100000)
    {
        Random random = new Random();
        double nextDouble = random.NextDouble() * (double)(max - min) + (double)min;
        return (decimal)nextDouble;
    }
}