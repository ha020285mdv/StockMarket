namespace StockMarket.Application.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string Email)
        : base($"Person with email '{Email}' was not found.")
    {
    }
}