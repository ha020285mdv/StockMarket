using MediatR;
using Microsoft.EntityFrameworkCore;
using StockMarket.Application.Exceptions;
using StockMarket.Database;
using StockMarket.Database.Model;

namespace StockMarket.Application.Queries;

public class SecurityQueryHandler: IRequestHandler<SecurityQuery, SecurityEntity>
{
    private readonly DatabaseContext _database;

    public SecurityQueryHandler(DatabaseContext database)
    {
        _database = database;
    }

    public async Task<SecurityEntity> Handle(SecurityQuery request, CancellationToken cancellationToken)
    {
        return await _database.Securities.FirstOrDefaultAsync(s => s.Ticker == request.Ticker, cancellationToken: cancellationToken) 
            ?? throw new SecurityNotFoundException(request.Ticker);
    }
}