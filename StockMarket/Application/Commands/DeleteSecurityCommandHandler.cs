
using MediatR;
using Microsoft.EntityFrameworkCore;
using StockMarket.Application.Exceptions;
using StockMarket.Database;

namespace StockMarket.Application.Commands;

public class DeleteSecurityCommandHandler : IRequestHandler<DeleteSecurityCommand>
{
    private readonly DatabaseContext _database;

    public DeleteSecurityCommandHandler(DatabaseContext database)
    {
        _database = database;
    }

     public async Task Handle(DeleteSecurityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _database.Securities.FirstOrDefaultAsync(p => p.Ticker == request.Ticker, cancellationToken: cancellationToken)
                     ?? throw new SecurityNotFoundException(request.Ticker);

        _database.Securities.Remove(entity);
        await _database.SaveChangesAsync(cancellationToken);
    }

}