namespace StockMarket.Application.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string Email)
        : base($"Person with personal identifier {Email} was not found.")
    {
    }
}