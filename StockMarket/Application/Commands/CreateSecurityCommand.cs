using MediatR;
using StockMarket.Database.Model;
using System.Net;


namespace StockMarket.Application.Commands
{
    public class CreateSecurityCommand: IRequest
    {
        public required string Ticker { get; set; }
        public required string Name { get; set; }
    }
}

