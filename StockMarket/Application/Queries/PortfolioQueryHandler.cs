using MediatR;
using Microsoft.EntityFrameworkCore;
using StockMarket.Application.Exceptions;
using StockMarket.Application.Model;
using StockMarket.Database;
using StockMarket.Database.Model;
using StockMarket.Services;

namespace StockMarket.Application.Queries;

public class PortfolioQueryHandler: IRequestHandler<PortfolioQuery, List<UserPortfolioData>>
{
    private readonly DatabaseContext _database;
    private readonly IUserService _userService;

    public PortfolioQueryHandler(DatabaseContext database, IUserService userService)
    {
        _database = database;
        _userService = userService;
    }

    public async Task<List<UserPortfolioData>> Handle(PortfolioQuery request, CancellationToken cancellationToken)
    {
        var user = _userService.GetUserSession();

        var q = _database.UsersPortfolios
            .Where(p => p.UserId == user.Id);

        if (request.Ticker != null) {
            q = q.Where(p => p.Ticker == request.Ticker);
        }

        return await q.Select(p => new UserPortfolioData() { Number = p.Number, Ticker = p.Ticker})
            .ToListAsync(cancellationToken);
    }
}