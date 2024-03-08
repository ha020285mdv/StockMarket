using MediatR;
using StockMarket.Database.Model;

namespace StockMarket.Application.Queries;

public class UserQuery: IRequest<UserEntity>
{
    public required string Email { get; set; }
}