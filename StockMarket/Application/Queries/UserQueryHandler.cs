using MediatR;
using StockMarket.Application.Exceptions;
using StockMarket.Database.Model;
using StockMarket.Services;

namespace StockMarket.Application.Queries;

public class UserQueryHandler: IRequestHandler<UserQuery, UserEntity>
{
    private readonly IUserService _userService;

    public UserQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UserEntity> Handle(UserQuery request, CancellationToken cancellationToken)
    {
        return 
            await _userService.GetUserByEmailAsync(request.Email, cancellationToken) 
            ?? throw new UserNotFoundException(request.Email);
    }
}