using MediatR;
using StockMarket.Application.Exceptions;
using StockMarket.Database.Model;
using StockMarket.Services;

namespace StockMarket.Application.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserEntity>
{
    private readonly IUserService _userService;

    public CreateUserCommandHandler(IUserService userService) { _userService = userService; }

    public async Task<UserEntity?> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        
        if (await _userService.GetUserByEmailAsync(request.Email, null) == null)
        { 
            throw new UserAlreadyExistsException(request.Email);
        }

        return await _userService.CreateUserAsync(
            request.Name,
            request.Email,
            GenerateSafeRandomString(30),
            request.Funds,
            cancellationToken
        );
    }

    public static string GenerateSafeRandomString(int length)
    {
        const string safeCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();

        string randomString = new string(Enumerable.Repeat(safeCharacters, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        return randomString;
    }
}