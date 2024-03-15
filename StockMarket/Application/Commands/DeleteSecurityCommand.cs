using MediatR;

namespace StockMarket.Application.Commands;

public class DeleteSecurityCommand : IRequest
{
    public required string Ticker { get; set; }
}