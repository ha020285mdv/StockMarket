using MediatR;
using StockMarket.Database.Model;

namespace StockMarket.Application.Queries;

public class SecurityQuery: IRequest<SecurityEntity>
{
    public required string Ticker { get; set; }
}