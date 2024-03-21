using MediatR;

namespace StockMarket.Application.Commands
{
    public class CreatePortfolioCommand: IRequest
    {
        public required string Ticker { get; set; }
        public required int Number { get; set; }
    }
}

