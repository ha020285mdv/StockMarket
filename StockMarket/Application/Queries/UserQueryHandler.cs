using MediatR;
using Microsoft.EntityFrameworkCore;
using StockMarket.Application.Exceptions;
using StockMarket.Database;
using StockMarket.Database.Model;

namespace StockMarket.Application.Queries;

public class UserQueryHandler: IRequestHandler<UserQuery, UserEntity>
{
    private readonly DatabaseContext _database;

    public UserQueryHandler(DatabaseContext database)
    {
        _database = database;
    }

    public async Task<UserEntity> Handle(UserQuery request, CancellationToken cancellationToken)
    {

        UserEntity? user = await _database.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken: cancellationToken);

        return await _database.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken: cancellationToken)
               ?? throw new UserNotFoundException(request.Email);
    }
}