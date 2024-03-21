using MediatR;
using StockMarket.Application.Model;
using StockMarket.Database.Model;

namespace StockMarket.Application.Queries;

public class PortfolioQuery: IRequest<List<UserPortfolioData>>
{
    public string? Ticker { get; set; }
}