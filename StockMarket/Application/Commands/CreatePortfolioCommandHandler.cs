using MediatR;
using Microsoft.EntityFrameworkCore;
using StockMarket.Application.Exceptions;
using StockMarket.Database;
using StockMarket.Database.Model;
using StockMarket.Services;

namespace StockMarket.Application.Commands;

public class CreatePortfolioCommandHandler : IRequestHandler<CreatePortfolioCommand>
{
    private readonly DatabaseContext _database;
    private readonly IUserService _userService;

    public CreatePortfolioCommandHandler(DatabaseContext database, IUserService userService)
    {
        _database = database;
        _userService = userService;
    }

    public async Task Handle(CreatePortfolioCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetUserSession().Id;
        var security = await _database.Securities.FirstOrDefaultAsync(s => s.Ticker == request.Ticker, cancellationToken: cancellationToken);
        if (security == null)
        {
            throw new SecurityNotFoundException(request.Ticker);
        }

        var entity = new UserPortfolioEntity
        {
            Number = request.Number,
            UserId = userId,
            Ticker = request.Ticker
        };

        _database.UsersPortfolios.Add(entity);
        await _database.SaveChangesAsync(cancellationToken);
    }
}