namespace StockMarket.Application.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException(string Email)
        : base($"Person with personal identifier {Email} already exists.")
    {
    }
}