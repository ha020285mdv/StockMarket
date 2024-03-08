using MediatR;
using StockMarket.Application.Exceptions;
using StockMarket.Database;
using StockMarket.Database.Model;

namespace StockMarket.Application.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserEntity>
{
    private readonly DatabaseContext _database;

    public CreateUserCommandHandler(DatabaseContext database)
    {
        _database = database;
    }

    public async Task<UserEntity> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (_database.Users.Any(p => p.Email == request.Email))
            throw new UserAlreadyExistsException(request.Email);

        var entity = new UserEntity
        {
            XAPIKey = GenerateSafeRandomString(30),
            Name = request.Name,
            Email = request.Email.ToLower(),
            Funds = request.Funds,
        };

        _database.Users.Add(entity);
        await _database.SaveChangesAsync(cancellationToken);

        return entity;
    }

    private static string GenerateSafeRandomString(int length)
    {
        const string safeCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();

        string randomString = new string(Enumerable.Repeat(safeCharacters, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        return randomString;
    }
}