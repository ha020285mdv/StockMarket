namespace StockMarket.Application.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException(string Email)
        : base($"Person with email '{Email}' already exists.")
    {
    }
}