using MediatR;
using Microsoft.EntityFrameworkCore;
using StockMarket.Database.Model;
using StockMarket.Database;
using StockMarket.Application.Queries;

namespace NationalPopulationRegister.Application.Queries;

public class SecuritiesQueryHandler : IRequestHandler<SecuritiesQuery, List<SecurityEntity>>
{
    private readonly DatabaseContext _database;

    public SecuritiesQueryHandler(DatabaseContext database)
    {
        _database = database;
    }

    public async Task<List<SecurityEntity>> Handle(SecuritiesQuery request, CancellationToken cancellationToken)
    {
        return await _database.Securities
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
    }
}