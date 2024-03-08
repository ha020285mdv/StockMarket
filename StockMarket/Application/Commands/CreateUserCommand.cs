using MediatR;
using StockMarket.Database.Model;
using System.Net;


namespace StockMarket.Application.Commands
{
    public class CreateUserCommand: IRequest<UserEntity>
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required decimal Funds { get; set; }

    }
}

