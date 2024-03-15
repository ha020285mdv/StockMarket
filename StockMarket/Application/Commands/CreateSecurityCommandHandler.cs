using MediatR;
using StockMarket.Application.Exceptions;
using StockMarket.Database;
using StockMarket.Database.Model;

namespace StockMarket.Application.Commands;

public class CreateSecurityCommandHandler : IRequestHandler<CreateSecurityCommand>
{
    private readonly DatabaseContext _database;

    public CreateSecurityCommandHandler(DatabaseContext database)
    {
        _database = database;
    }

    public async Task Handle(CreateSecurityCommand request, CancellationToken cancellationToken)
    {
        if (_database.Securities.Any(p => p.Ticker == request.Ticker))
            throw new SecurityAlreadyExistsException(request.Ticker);

        var entity = new SecurityEntity
        {
            Ticker = request.Ticker,
            Name = request.Name
        };

        _database.Securities.Add(entity);
        await _database.SaveChangesAsync(cancellationToken);
    }
}